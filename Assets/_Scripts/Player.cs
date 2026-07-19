using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _playerSpeed = 5;

    Vector3 _lastMoveDir;

    float _playerInteractDistance = 2;
    float _playerRotateSpeed = 5;

    CharacterController _characterController;

    ICanInteract _selectedInteractSite;

    PartObject _heldPartObject;

    bool _isWalking;

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

        _isWalking = moveDir != Vector3.zero; // true if move dir is not zero

        _characterController.Move(moveDir * _playerSpeed * Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _playerRotateSpeed);
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
