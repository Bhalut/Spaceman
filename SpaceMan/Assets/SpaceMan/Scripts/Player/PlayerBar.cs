using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public BarType type;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        switch (type)
        {
            case BarType.HealthBar:
                slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case BarType.ManaBar:
                slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }

    private void Update()
    {
        switch (type)
        {
            case BarType.HealthBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealth();
                break;

            case BarType.ManaBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();
                break;
        }
    }
}

public enum BarType
{
    HealthBar,
    ManaBar
}
