using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;
    private PlayerController player;
    public int collectedObject = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetGameState(GameState.Menu);
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Menu:
                UiManager.instance.ShowMainMenu();
                UiManager.instance.HideGameMenu();
                UiManager.instance.HideGameOverMenu();
                break;
            case GameState.InGame:
                LevelManager.instance.RemoveAllLevelBlock();
                LevelManager.instance.GenerateInitBlock();
                UiManager.instance.HideMainMenu();
                UiManager.instance.ShowGameMenu();
                UiManager.instance.HideGameOverMenu();
                player.Init();
                break;
            case GameState.GameOver:
                UiManager.instance.HideMainMenu();
                UiManager.instance.HideGameMenu();
                UiManager.instance.ShowGameOverMenu();
                break;
            default:
                goto case GameState.Menu;
        }

        this.state = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}

public enum GameState
{
    Menu,
    InGame,
    GameOver
}
