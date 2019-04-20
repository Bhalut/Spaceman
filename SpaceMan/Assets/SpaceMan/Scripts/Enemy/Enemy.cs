using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float speed = 1.5f;
    private int damage = 10;
    private bool facingRight = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    private void Start()
    {
        this.transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        float currentSpeed = speed;

        if (facingRight)
        {
            currentSpeed = speed;
            this.transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            currentSpeed = -speed;
            this.transform.eulerAngles = Vector2.zero;
        }


        if (GameManager.instance.state == GameState.InGame)
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().
                     CollectHealth(-damage);
            return;
        }

        if (collision.CompareTag("Coin"))
        {
            return;
        }

        facingRight = !facingRight;
    }
}
