using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            StartGame();
        }
    }

    private void Init()
    {
        state = GameState.Menu;
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
                break;
            case GameState.InGame:
                break;
            case GameState.GameOver:
                break;
            default:
                goto case GameState.Menu;
        }

        this.state = newGameState;
    }
}

public enum GameState
{
    Menu,
    InGame,
    GameOver
}
