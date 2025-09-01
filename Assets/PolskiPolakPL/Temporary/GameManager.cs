using UnityEngine;
using PolskiPolakPL.Utils;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public GameObject PlayerGO;

    [SerializeField] float lobbyTime = 10;
    [SerializeField] float endgameTime = 5;
    [SerializeField] KeyCode skipStateKey = KeyCode.Tab;
    [SerializeField] TMP_Text messageTextField;


    Timer lobbyTimer;
    Timer endgameTimer;
    Timer uiTimer;
    public static GameState gameState { get; private set; } = GameState.LOBBY;
    private bool gameStateChanged = false;
    string logMessage = "";
    private void Start()
    {
        Application.targetFrameRate = 120;
        lobbyTimer = new Timer(lobbyTime);
        lobbyTimer.OnTimerElapsed += StartGame;
        endgameTimer = new Timer(endgameTime);
        endgameTimer.OnTimerElapsed += ResetGame;
        uiTimer = new Timer(1);
        uiTimer.OnTimerElapsed += UpdateUIText;
    }

    private void UpdateUIText()
    {
        if (messageTextField)
            messageTextField.text = logMessage;
        else
            Debug.Log(logMessage);
    }

    private void OnDestroy()
    {
        lobbyTimer.OnTimerElapsed -= StartGame;
        endgameTimer.OnTimerElapsed -= ResetGame;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.LOBBY:
                {
                    HandleLobbyState();
                }
                break;

            case GameState.IN_GAME:
                {
                    HandleInGameState();
                }
                break;

            case GameState.END_GAME:
                {
                    HandleEndGameState();
                }
                break;

            default: { Debug.LogWarning("ANOTHER GAME STATE?!?!?!?!"); }break;
        }
        uiTimer.Tick(Time.deltaTime);
    }

    private void HandleLobbyState()
    {
        logMessage = $"[{gameState}] Game starts in: {(int)lobbyTimer.RemaningSeconds}s";
        if (Input.GetKeyDown(skipStateKey))
        {
            GameManager.Instance.ChangeState(GameState.IN_GAME);
        }
        lobbyTimer.Tick(Time.deltaTime);
    }

    private void HandleInGameState()
    {
        logMessage = $"[{gameState}] Current Tagged Player: {PlayerGO.name}";
        if (Input.GetKeyDown(skipStateKey))
        {
            GameManager.Instance.ChangeState(GameState.END_GAME);
        }
    }

    private void HandleEndGameState()
    {
        logMessage = $"[{gameState}] Game resets in: {(int)endgameTimer.RemaningSeconds}s";
        if (Input.GetKeyDown(skipStateKey))
        {
            GameManager.Instance.ChangeState(GameState.LOBBY);
        }
        endgameTimer.Tick(Time.deltaTime);
    }

    public void ChangeState(GameState newState)
    {
        GameManager.gameState = newState;
    }

    void StartGame()
    {
        GameManager.Instance.ChangeState(GameState.IN_GAME);
    }

    void ResetGame()
    {
        GameManager.Instance.ChangeState(GameState.LOBBY);
    }
}

public enum GameState
{
    LOBBY,
    IN_GAME,
    END_GAME
}