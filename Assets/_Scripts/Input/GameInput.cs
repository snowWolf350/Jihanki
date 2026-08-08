using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    public event EventHandler OnEPressed;
    public event EventHandler OnFPressed;
    public event EventHandler OnAnyKeyPressed;
    public event EventHandler OnEscapePressed;

    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerInput();
    }
    private void Start()
    {
        _playerInput.player.interact.performed += Interact_performed;
        _playerInput.player.altinteract.performed += Altinteract_performed;
        _playerInput.player.anyButton.performed += AnyButton_performed;
        _playerInput.player.escape.performed += Escape_performed;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEscapePressed?.Invoke(this, EventArgs.Empty);
    }

    private void AnyButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAnyKeyPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Altinteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFPressed?.Invoke(this, EventArgs.Empty);  
    }

    public Vector2 GetInputVector()
    {
        Vector2 inputVector = _playerInput.player.move.ReadValue<Vector2>();
        return inputVector;
    }
}
