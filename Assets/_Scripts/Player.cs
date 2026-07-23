using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _playerSpeed = 5;

    Vector3 _lastMoveDir;
    Vector3 _rotateDir;

    float _playerInteractDistance = 2;
    float _playerRotateSpeed = 5;

    CharacterController _characterController;

    ICanInteract _selectedInteractSite;

    PartObject _heldPartObject;

    bool _isWalking;
    bool _isGrounded;

    float verticalVelocity;
    float _gravity = -9.8f;

    public static event EventHandler<InteractableSiteEventArgs> OnInteractableSiteChanged;

    public class InteractableSiteEventArgs : EventArgs
    {
        public ICanInteract interactale;
    }

    [SerializeField] Transform _holdTransform;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        GameInput.Instance.OnEPressed += Input_OnEPressed;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractionRaycast();
    }
    void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        _rotateDir = moveDir;
        _isGrounded = _characterController.isGrounded;

        if (_characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // keeps player grounded
        }
        else
        {
            verticalVelocity += _gravity * Time.deltaTime;
        }


        _isWalking = moveDir != Vector3.zero; // true if move dir is not zero

        moveDir.y = verticalVelocity;

        _characterController.Move(moveDir * _playerSpeed * Time.deltaTime);

        if (_rotateDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(
                transform.forward,
                _rotateDir,
                Time.deltaTime * _playerRotateSpeed);
        }
    }
    void HandleInteractionRaycast()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastMoveDir = moveDir;
        }
        if (Physics.Raycast(transform.position, _lastMoveDir, out RaycastHit hitCounter, _playerInteractDistance))
        {
            if (hitCounter.transform.TryGetComponent(out ICanInteract interactable))
            {
                if (_selectedInteractSite == interactable) return;

                SetInteractableSiteTo(interactable);
            }
        }
        else
        {
            if (_selectedInteractSite == null) return;
            SetInteractableSiteTo(null);
        }
    }

    private void Input_OnEPressed(object sender, System.EventArgs e)
    {
        if (_selectedInteractSite == null) return; // no hovered site is there
        if (_heldPartObject != null) return; // player is aldready carrying something
        _selectedInteractSite.OnInteract(this);
    }

    public void SetInteractableSiteTo(ICanInteract interactable)
    {
        _selectedInteractSite = interactable;

        OnInteractableSiteChanged?.Invoke(this, new InteractableSiteEventArgs
        {
            interactale = interactable,
        });
    }

    public bool IsPlayerWalking()
    {
        return _isWalking;
    }

    public Transform GetHoldTransform()
    {
        return _holdTransform;
    }
}
