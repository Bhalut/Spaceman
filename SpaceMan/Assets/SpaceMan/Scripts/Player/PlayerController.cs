using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 6f;
    private float speed = 2f;
    private bool isJump;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    private Animator anim;
    private const string STATE_ALIVE = "isAlive";
    private const string STATE_GROUND = "isGround";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        isJump = Input.GetKeyDown(KeyCode.Space);
       

        State();

        Debug.DrawRay(transform.position, Vector2.down * 1.45f, Color.gray);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x < speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (isJump)
        {
            Jump();
        }
    }

    private void State()
    {
        anim.SetBool(STATE_GROUND, IsTouchingOnTheGround());
    }

    private void Jump()
    {
        if (IsTouchingOnTheGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Init()
    {
        anim.SetBool(STATE_ALIVE, true);
        anim.SetBool(STATE_GROUND, false);
    }

    private bool IsTouchingOnTheGround() => Physics2D.Raycast(transform.position, Vector2.down, 1.45f, groundMask);
}
