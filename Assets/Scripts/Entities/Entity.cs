using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    private Rigidbody rigid;
    public Animator anim;
    public Health health;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform attackPoint;

    public LayerMask pushableLayers;
    private GameObject curPushable;

    public float moveSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float jumpForce = 2.5f;

    public LayerMask attackableLayers;
    public float attackRadius = 0.5f;
    public int attackDamage = 5;

    private float turnTime = 0.1f;
    private float turnVel;

    private Vector2 curDir = Vector2.zero;
    private float dirInfluence = 0f;
    public bool running = false;
    public bool pushing = false;

    public EStates curState = EStates.idle;
    public bool disabled = false;

    public enum EStates
    {
        disabled,
        idle,
        falling,
        walking,
        running,
        pushing,
        dead,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DetermineState();
    }

    protected virtual void FixedUpdate()
    {
        HandleMove();
    }

    public void Move(Vector2 dir)
    {
        curDir = dir;
    }

    public void InfluenceMove(float influence)
    {
        dirInfluence = influence;
    }

    private void HandleMove()
    {
        if (curState != EStates.dead && curState != EStates.disabled) {
            Vector3 normalDir = new Vector3(curDir.x, 0f, curDir.y).normalized;

            if (normalDir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(normalDir.x, normalDir.z) * Mathf.Rad2Deg + dirInfluence;
                float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref turnVel, turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 vel = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                HandlePush(vel, targetAngle);

                if (curState == EStates.pushing & curPushable != null)
                {
                    vel *= moveSpeed;
                    anim.SetBool("pushing", true);
                    anim.SetBool("walking", false);
                    anim.SetBool("running", false);
                }
                else if (curState == EStates.running)
                {
                    vel *= runSpeed;
                    anim.SetBool("walking", false);
                    anim.SetBool("pushing", false);
                    anim.SetBool("running", true);
                }
                else
                {
                    vel *= moveSpeed;
                    anim.SetBool("running", false);
                    anim.SetBool("pushing", false);
                    anim.SetBool("walking", true);
                }

                vel.y = rigid.linearVelocity.y;

                if (curState == EStates.falling)
                {
                    StopMoveAnims();
                    rigid.AddForce(vel);
                }
                else
                {
                    rigid.linearVelocity = vel;
                }
            }
            else
            {
                StopMoveAnims();
            }
        } else
        {
            StopMoveAnims();
        }
        
    }

    private void StopMoveAnims()
    {
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetBool("pushing", false);
    }

    private void HandlePush(Vector3 fVector, float tAngle)
    {
        if (curPushable == null && pushing)
        {
            Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRadius, pushableLayers);

            foreach (Collider hitObject in hitObjects)
            {
                if (hitObject.CompareTag("Pushable"))
                {
                    curPushable = hitObject.gameObject;
                    break;
                }
            }
        }
        else if (pushing)
        {
            Vector3 newPos = transform.position + fVector;
            newPos.y = curPushable.transform.position.y;
            curPushable.transform.rotation = Quaternion.Euler(0f, tAngle, 0f);
            curPushable.transform.position = newPos;
        }
        else
        {
            curPushable = null;
        }
    }

    public void Jump()
    {
        if(curState != EStates.dead && curState != EStates.disabled && !pushing){
            //Check if Jump is Possible
            if (curState != EStates.falling)
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

    }

    public void Attack()
    {
        string curAnim = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (curState != EStates.dead && curState != EStates.disabled && curAnim != "TutBotPunch" && !pushing && Player.singleton.curState != EStates.dead)
        {
            anim.Play("TutBotPunch");

            Collider[] hitEntitys = Physics.OverlapSphere(attackPoint.position, attackRadius, attackableLayers);

            foreach (Collider hitEntity in hitEntitys)
            {
                Health eHealth = hitEntity.GetComponent<Health>();
                if (eHealth != null)
                {
                    print(health.TeamID);
                    if (health == null || eHealth.TeamID != health.TeamID)
                    {
                        Debug.Log($"{hitEntity.name} got hit by {this.name}, dealed {attackDamage} Damage");
                        eHealth.Damage(attackDamage);
                    }
                }
            }

            //Damage the health script of the Entity}

        }
    }

    public void Damaged()
    {
        //Something happens when Entity is Damaged maybe an animation?
    }

    public virtual void Die()
    {
        curState = EStates.dead;
        anim.enabled = false;
    }

    public virtual void DetermineState()
    {
        if (curState != EStates.dead & !disabled)
        {
            if (!Physics.Raycast(groundCheck.position, -1 * transform.up, .1f))
            {
                curState = EStates.falling;
            }
            else if (rigid.linearVelocity.magnitude > 0)
            {
                if (pushing)
                {
                    curState = EStates.pushing;
                }
                else if (running)
                {
                    curState = EStates.running;
                }
                else
                {
                    curState = EStates.walking;
                }
            }
            else
            {
                curState = EStates.idle;
            }

        }
        else if (disabled)
        {
            curState = EStates.disabled;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
