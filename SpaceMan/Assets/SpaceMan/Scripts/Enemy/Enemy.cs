using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float speed;
    private bool facingRight;

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

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Die
        }
    }
}
