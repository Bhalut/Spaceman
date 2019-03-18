using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;
    private PlayerController player;

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

    private void Update()
    {
        if (state != GameState.InGame)
        {
            if (Input.GetKey(KeyCode.S))
            {
                StartGame();
            }
        }
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
                break;
            case GameState.InGame:
                LevelManager.instance.RemoveAllLevelBlock();
                LevelManager.instance.GenerateInitBlock();
                player.Init();
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
