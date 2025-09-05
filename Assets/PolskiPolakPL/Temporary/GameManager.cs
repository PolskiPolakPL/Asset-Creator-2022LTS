using UnityEngine;
using PolskiPolakPL.Utils;
using TMPro;
using System.Collections.Generic;
using System;

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
    [SerializeField] List<PlayerScript> playersList;
    [SerializeField] PlayerScript taggedPlayer;

    [Header("Game States")]
    [SerializeField] float lobbyTime = 10;
    [SerializeField] float endgameTime = 5;
    [SerializeField] KeyCode skipStateKey = KeyCode.Tab;
    public static GameState gameState { get; private set; } = GameState.LOBBY;
    public event Action<GameState> OnGameStateEnter;
    public event Action<GameState> OnGameStateUpdate;
    public event Action<GameState> OnGameStateExit;

    [Header("UI")]
    [SerializeField] TMP_Text messageTextField;
    [SerializeField] TMP_Text playersListTextField;
    string logMessage = "";

    int poolSize = 0;

    Timer lobbyTimer;
    Timer endgameTimer;
    Timer uiTimer;

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
        OnGameStateUpdate?.Invoke(gameState);
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
        logMessage = $"[{gameState}] Current Tagged Player: {taggedPlayer.gameObject.name}";
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
        OnGameStateExit?.Invoke(gameState);
        switch (newState)
        {
            case GameState.LOBBY:
                {
                    if (playersListTextField)
                        UpdatePlayersList();
                }
                break;

            case GameState.IN_GAME:
                {
                    taggedPlayer = GetRandomPlayer(playersList);
                }
                break;

            case GameState.END_GAME:
                {
                    foreach(PlayerScript player in playersList)
                    {
                        if(player == taggedPlayer)
                        {
                            player.ticket.SetValue(player.ticket.minValue);
                        }
                        else
                        {
                            player.ticket.Add(10);
                        }
                        Debug.Log($"{player.name} has now {player.ticket.value} tickets");
                    }
                }
                break;

            default: { Debug.LogWarning("ANOTHER GAME STATE?!?!?!?!"); } break;
        }
        UpdatePlayersList();
        GameManager.gameState = newState;
        OnGameStateEnter?.Invoke(newState);
    }

    void UpdatePlayersList()
    {
        poolSize = GetPoolSize();
        playersListTextField.text = "";
        foreach (PlayerScript player in playersList)
        {
            playersListTextField.text += $"{player.name} ({Mathf.RoundToInt(((float)player.ticket.value / (float)poolSize) * 100)}%)\n";
        }
    }

    private int GetPoolSize()
    {
        int pool = 0;
        foreach(PlayerScript player in playersList)
        {
            pool += player.ticket.value;
        }
        return pool;
    }

    private PlayerScript GetRandomPlayer(List<PlayerScript> playersList)
    {
        PlayerScript choosenPlayer = playersList[0];
        poolSize = GetPoolSize();
        int randomVal = UnityEngine.Random.Range(1, poolSize);
        foreach(PlayerScript player in playersList)
        {
            randomVal -= player.ticket.value;
            if (randomVal <= 0)
            {
                choosenPlayer = player;
                break;
            }
        }
        return choosenPlayer;
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