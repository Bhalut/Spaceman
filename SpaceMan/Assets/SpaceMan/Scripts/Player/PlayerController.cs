using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 10f;
    private float speed = 2f;
    private bool _isJump;
    private Rigidbody2D _rb;
    public LayerMask groundMask;
    private Animator _anim;
    private Vector2 _startPosition;
    private const string StateAlive = "isAlive";
    private const string StateGround = "isGround";
    private const string StateDie = "isDie";

    private int _healthPoints, _manaPoints;
    private static readonly int IsAlive = Animator.StringToHash(StateAlive);
    private static readonly int IsGround = Animator.StringToHash(StateGround);

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _startPosition = this.transform.position;
        Init();
    }

    private void Update()
    {
        #if UNITY_ANDROID || UNITY_IOS
        _isJump = Input.GetButtonDown("Fire1");
        #else
        _isJump = Input.GetButtonDown("Fire1");
        #endif

        Debug.DrawRay(transform.position, Vector2.down * 1.45f, Color.gray);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.state == GameState.InGame)
        {
            if (_rb.velocity.x < speed)
            {
                _rb.velocity = new Vector2(speed, _rb.velocity.y);
            }

            if (_isJump)
            {
                Jump();
            }
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

    }

    private void Jump()
    {
        if (IsTouchingOnTheGround())
        {
            GetComponent<AudioSource>().Play();
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _anim.SetBool(IsGround, IsTouchingOnTheGround());
        }
    }

    public void Init()
    {
        _anim.SetBool(IsAlive, true);
        _anim.SetBool(IsGround, false);

        _healthPoints = INITIAL_HEALTH;
        _manaPoints = INITIAL_MANA;

        Invoke(nameof(RestartPosition), 0.1f);

        GameObject cam = GameObject.FindWithTag("MainCamera");
        cam.GetComponent<CameraController>().ResetCameraPosition();
    }

    private void RestartPosition()
    {
        this.transform.position = _startPosition;
        this._rb.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraController>().ResetCameraPosition();
    }

    private bool IsTouchingOnTheGround() => Physics2D.Raycast(transform.position, Vector2.down, 1.45f, groundMask);

    public void Die()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }

        this._anim.SetBool(IsAlive, false);
        this._anim.SetTrigger(StateDie);
        GameManager.instance.GameOver();
    }

    public void CollectHealth(int points)
    {
        this._healthPoints += points;
        if (this._healthPoints >= MAX_HEALTH)
        {
            this._healthPoints = MAX_HEALTH;
        }

        if (this._healthPoints <= 0)
        {
            Die();
        }
    }

    public void CollectMana(int points)
    {
        this._manaPoints += points;
        if (this._manaPoints >= MAX_MANA)
        {
            this._manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return _healthPoints;
    }

    public int GetMana()
    {
        return _manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - _startPosition.x;
    }
}
