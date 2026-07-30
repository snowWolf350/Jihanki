using System;
using UnityEngine;

public class Player : MonoBehaviour,IPartParent
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
    bool _canWalk = true;
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
        GameInput.Instance.OnFPressed += Input_OnFPressed;

        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void Update()
    {
        if (_canWalk == false) return;

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

    private void Input_OnFPressed(object sender, EventArgs e)
    {
        if (_selectedInteractSite == null) return;

        _selectedInteractSite.OnAltInteract(this);
    }
    private void Input_OnEPressed(object sender, System.EventArgs e)
    {
        if (_selectedInteractSite == null) return; // no hovered site is there

        _selectedInteractSite.OnInteract(this);
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        _canWalk = !GameManager.Instance.IsGameInMenu(); // if game is in menu he cannot walk
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

    public void SetPartObject(PartObject partObject)
    {
        _heldPartObject = partObject;
    }

    public bool TryGetHeldPartObject(out PartObject partobject)
    {
        if (_heldPartObject == null)
        {
            partobject = null;
            return false;
        }
        partobject = _heldPartObject;
        return true;
    }


    public Transform GetPlacementTransform()
    {
        return _holdTransform;
    }

    public void SetPartObjectTo(PartObject partobject)
    {
        _heldPartObject = partobject;
    }
}
