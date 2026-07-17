using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public Vector2 GetInputVector()
    {
        Vector2 inputVector = _playerInput.player.move.ReadValue<Vector2>();
        return inputVector;
    }
}
