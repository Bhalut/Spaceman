using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.Coin;

    private SpriteRenderer sprite;
    private CircleCollider2D circleCollider;
    private bool hasBeenCollected;

    public int value = 1;

    private GameObject player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Show()
    {
        sprite.enabled = true;
        circleCollider.enabled = true;
        hasBeenCollected = false;
    }

    private void Hide()
    {
        sprite.enabled = false;
        circleCollider.enabled = false;

    }

    private void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (type)
        {
            case CollectableType.Coin:
                GameManager.instance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
            case CollectableType.HealthPotion:
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.ManaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
            default:
                goto case CollectableType.Coin;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}

public enum CollectableType
{
    HealthPotion,
    ManaPotion,
    Coin
}
