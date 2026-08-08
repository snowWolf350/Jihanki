using System;
using UnityEngine;

public class GameManager : MonoBehaviour , IHasProgress
{
    public static GameManager Instance;

    public static event EventHandler OnGameStateChanged;
    public static event EventHandler OnMenuOpened;
    public static event EventHandler OnDayChanged;
    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    float _dayTimer;
    float _dayTimerMax = 120;

    int _DaysPassed = 0;

    bool _isPaused;

    enum GameState
    {
        Playing,Menu,Paused,dayOver
    }

    GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }
    private void Start()
    {
        GameInput.Instance.OnEscapePressed += Instance_OnEscapePressed;
    }
    private void Update()
    {
        _dayTimer += Time.deltaTime;

        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = 1 - _dayTimer / _dayTimerMax
        });

        if (_dayTimer > _dayTimerMax)
        {
            _dayTimer = 0;
            _DaysPassed++;
            setCurrentGameStateTo(GameState.dayOver);

            OnDayChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnEscapePressed -= Instance_OnEscapePressed;
    }
    private void Instance_OnEscapePressed(object sender, System.EventArgs e)
    {
        if(_isPaused) // time to unoause
        setCurrentGameStateTo(GameState.Playing);
        else // time to pause
        setCurrentGameStateTo(GameState.Paused);


        _isPaused = !_isPaused;
    }

    public void SetGameStateToMenu()
    {

        setCurrentGameStateTo(GameState.Menu);
    }
    public void SetGameStateToPlaying()
    {
        setCurrentGameStateTo(GameState.Playing);

    }

    void setCurrentGameStateTo(GameState gameState)
    {
        if (gameState == GameState.Paused)
        {
            Time.timeScale = 0;
        }
        else
        {

            Time.timeScale = 1;
        }

        if (gameState == GameState.Menu)
        {
            OnMenuOpened?.Invoke(this, EventArgs.Empty);
        }

        _currentGameState = gameState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetDaysPassed()
    {
        return _DaysPassed;
    }
    public bool IsGameInMenu()
    {
        return _currentGameState == GameState.Menu;
    }
    public bool IsGamePaused()
    {
        return _currentGameState == GameState.Paused;
    }
    public bool IsGamePlaying()
    {
        return _currentGameState == GameState.Playing;
    }
}
