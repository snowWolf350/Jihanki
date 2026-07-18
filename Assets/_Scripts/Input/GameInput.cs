using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    public event EventHandler OnEPressed;

    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerInput();
    }
    private void Start()
    {
        _playerInput.player.interact.performed += Interact_performed;
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEPressed?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetInputVector()
    {
        Vector2 inputVector = _playerInput.player.move.ReadValue<Vector2>();
        return inputVector;
    }
}
