using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event EventHandler OnGameStateChanged;

    enum GameState
    {
        Playing,Menu,Paused
    }

    GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    public void SetGameStateToMenu()
    {
        _currentGameState = GameState.Menu;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetGameStateToPlaying()
    {
        _currentGameState = GameState.Playing;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool IsGameInMenu()
    {
        return _currentGameState == GameState.Menu;
    }
}
