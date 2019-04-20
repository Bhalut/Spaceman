using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.AddLevelBlock();
            LevelManager.instance.RemoveLevelBlock();
        }
    }
}
