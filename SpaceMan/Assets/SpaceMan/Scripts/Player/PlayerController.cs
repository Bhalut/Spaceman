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
    private const string STATE_DIE = "isDie";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        isJump = Input.GetKeyDown(KeyCode.Space);
       

        State();

        Debug.DrawRay(transform.position, Vector2.down * 1.45f, Color.gray);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.state == GameState.InGame)
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
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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

    public void Die()
    {
        anim.SetTrigger(STATE_DIE);
        anim.SetBool(STATE_ALIVE, false);
        GameManager.instance.GameOver();
    }
}
