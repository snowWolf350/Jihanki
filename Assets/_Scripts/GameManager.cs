using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    enum GameState
    {
        Playing,Menu,Paused
    }

    GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }
}
