using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Canvas canvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowMainMenu()
    {
        canvas.enabled = true;
    }

    public void HideMainMenu()
    {
        canvas.enabled = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
