using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    public event EventHandler OnEPressed;
    public event EventHandler OnAnyKeyPressed;

    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerInput();
    }
    private void Start()
    {
        _playerInput.player.interact.performed += Interact_performed;
        _playerInput.player.anyButton.performed += AnyButton_performed;
    }
    private void OnDestroy()
    {
        _playerInput.player.interact.performed -= Interact_performed;
        _playerInput.player.anyButton.performed -= AnyButton_performed;

        Instance = null;
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void AnyButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAnyKeyPressed?.Invoke(this, EventArgs.Empty);
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
