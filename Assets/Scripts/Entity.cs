using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator anim;
    [SerializeField] private Transform groundCheck;

    public float moveSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float jumpForce = 2.5f;

    private float turnTime = 0.1f;
    private float turnVel;

    public EStates curState = EStates.idle;
    public bool disabled = false;

    public enum EStates
    {
        disabled,
        idle,
        moving,
        dead,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DetermineState();
    }

    public void Move(Vector2 dir, float influnce = 0f, bool run = false)
    {
        Vector3 normalDir = new Vector3(dir.x, 0f, dir.y).normalized;

        if (normalDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(normalDir.x, normalDir.z) * Mathf.Rad2Deg + influnce;
            float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref turnVel, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 vel = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (run)
            {
                vel *= runSpeed;
                anim.SetBool("walking", false);
                anim.SetBool("running", true);
            }
            else
            {
                vel *= moveSpeed;
                anim.SetBool("running", false);
                anim.SetBool("walking", true);
            }

            vel.y = rigid.linearVelocity.y;

            rigid.linearVelocity = vel;
        }
        else
        {
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
        }
    }

    public void Jump()
    {
        //Check if Jump is Possible
        if (Physics.Raycast(groundCheck.position, -1 * transform.up, .1f))
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Attack()
    {
        anim.Play("TutBotPunch");
        //Damage the health script of the Entity
    }

    public void Damaged()
    {
        //Something happens when Entity is Damaged maybe an animation?
    }

    public void Die()
    {
        curState = EStates.dead;
        if (gameObject.tag == "Player")
        {
            Player player = gameObject.GetComponent<Player>();
            if (player != null)
            {
                //Maybe die differently if its a player
                player.disabled = true;
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void DetermineState()
    {
        if (curState != EStates.dead & !disabled)
        {
            if (rigid.linearVelocity.magnitude > 0)
            {
                curState = EStates.moving;
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
}
