using UnityEngine;
using PolskiPolakPL.Utils;
using TMPro;
using System.Collections.Generic;
using System;

public class TagGameManager : MonoBehaviour
{
    //Singleton
    public static TagGameManager Instance;
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
    [SerializeField] float lobbyTime = 3;
    [SerializeField] float gameTime = 5;
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
    Timer gameTimer;
    Timer uiTimer;

    private void Start()
    {
        Application.targetFrameRate = 120;
        lobbyTimer = new Timer(lobbyTime);
        lobbyTimer.OnTimerElapsed += StartGame;
        gameTimer = new Timer(gameTime);
        gameTimer.OnTimerElapsed += ResetGame;
        uiTimer = new Timer(.1f);
        uiTimer.OnTimerElapsed += UpdateUIText;
        uiTimer.Tick(.1f);
        UpdatePlayersList();
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
        gameTimer.OnTimerElapsed -= ResetGame;
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
        }
        uiTimer.Tick(Time.deltaTime);
        OnGameStateUpdate?.Invoke(gameState);
    }

    private void HandleLobbyState()
    {
        logMessage = $"[{gameState}] Game starts in: {Mathf.RoundToInt(lobbyTimer.RemaningSeconds)}s";
        if (Input.GetKeyDown(skipStateKey))
        {
            ChangeState(GameState.IN_GAME);
        }
        lobbyTimer.Tick(Time.deltaTime);
    }

    private void HandleInGameState()
    {
        logMessage = $"[{gameState}] Current Tagged Player: {taggedPlayer.gameObject.name}\n" +
            $"Game ends in: {Mathf.RoundToInt(gameTimer.RemaningSeconds)}s";
        if (Input.GetKeyDown(skipStateKey))
        {
            ChangeState(GameState.LOBBY);
        }
        gameTimer.Tick(Time.deltaTime);
    }

    public void ChangeState(GameState newState)
    {
        OnGameStateExit?.Invoke(gameState);
        switch (newState)
        {
            case GameState.LOBBY:
                {
                    EvaluateTickets();
                    UpdatePlayersList();
                }
                break;

            case GameState.IN_GAME:
                {
                    taggedPlayer = GetRandomPlayer(playersList);
                    UpdatePlayersList();
                }
                break;
        }
        gameState = newState;
        OnGameStateEnter?.Invoke(newState);
    }

    private void EvaluateTickets()
    {
        int playerMisses;
        int playerCount = playersList.Count;
        foreach (PlayerScript player in playersList)
        {
            if (player == taggedPlayer)
            {
                player.tagTicket.SetWeight(player.tagTicket.MinWeight);
                player.tagTicket.misses = 0;
            }
            else
            {
                player.tagTicket.misses++;
                playerMisses = player.tagTicket.misses;
                if (playerCount > 10 && playerMisses > Mathf.RoundToInt(playerCount / 2))
                {
                    player.tagTicket.Add(2);
                }
                else
                {
                    player.tagTicket.Add(1);
                }
            }
            Debug.Log($"{player.name} has now {player.tagTicket.Weight} tickets");
        }
    }

    void UpdatePlayersList()
    {
        poolSize = GetPoolSize();
        playersListTextField.text = "";
        int chancePercent;
        foreach (PlayerScript player in playersList)
        {
            chancePercent = Mathf.RoundToInt(((float)player.tagTicket.Weight / (float)poolSize) * 100);
            playersListTextField.text += $"[{player.tagTimes}] {player.name} ({chancePercent}%)\n";
        }
    }

    private int GetPoolSize()
    {
        int pool = 0;
        foreach(PlayerScript player in playersList)
        {
            pool += player.tagTicket.Weight;
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
            randomVal -= player.tagTicket.Weight;
            if (randomVal <= 0)
            {
                choosenPlayer = player;
                player.tagTimes++;
                break;
            }
        }
        return choosenPlayer;
    }

    void StartGame()
    {
        ChangeState(GameState.IN_GAME);
    }

    private void ResetGame()
    {
        ChangeState(GameState.LOBBY);
    }
}

public enum GameState
{
    LOBBY,
    IN_GAME
}