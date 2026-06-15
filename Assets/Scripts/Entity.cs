using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float runSpeed = 5.0f;
    public float jumpSpeed = 5.0f;

    private Rigidbody rigid;
    [SerializeField] private Transform groundCheck;

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
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //DetermineState();
    }

    public void Move(Vector2 moveVel, bool run = false)
    {
        Vector3 vel = Vector3.zero;
        vel += transform.right * moveVel.x;
        vel += transform.forward * moveVel.y;

        if (run)
        {
            vel *= runSpeed;
        }
        else
        {
            vel *= moveSpeed;
        }

        vel.y = rigid.linearVelocity.y;

        rigid.linearVelocity = vel;

        float forwardVal = Vector3.Dot(vel, transform.forward);
        //anim.SetFloat("move", forwardVal);
    }



    public void Jump()
    {
        Vector3 vel = rigid.linearVelocity;

        //Check if Jump is Possible
        if (Physics.Raycast(groundCheck.position, -1 * transform.up, .1f))
        {
            vel += transform.up * jumpSpeed;
        }

        rigid.linearVelocity = vel;
    }
}
