using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _playerSpeed = 5;

    Vector3 _lastMoveDir;

    float _playerInteractDistance = 2;
    float _playerRotateSpeed = 5;

    CharacterController _characterController;

    PartSite _hoveredPartSite;

    PartObject _heldPartObject;

    bool _isWalking;

    public static event EventHandler<PartSiteEventArgs> OnPartSiteChanged;

    public class PartSiteEventArgs : EventArgs
    {
        public PartSite site;
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
            if (hitCounter.transform.TryGetComponent(out PartSite partSite))
            {
                if (_hoveredPartSite == partSite) return;

                SetPartSite(partSite);
            }
        }
        else
        {
            if (_hoveredPartSite == null) return;
            SetPartSite(null);
        }
    }

    private void Input_OnEPressed(object sender, System.EventArgs e)
    {
        if (_hoveredPartSite == null) return; // no hovered site is there
        if (_heldPartObject != null) return; // player is aldready carrying something

        _heldPartObject = _hoveredPartSite.GetPartObject();
        _heldPartObject.SetParentTo(_holdTransform);
    }

    public void SetPartSite(PartSite partsite)
    {
        _hoveredPartSite = partsite;

        OnPartSiteChanged?.Invoke(this, new PartSiteEventArgs
        {
            site = partsite,
        });
    }

    public bool IsPlayerWalking()
    {
        return _isWalking;
    }
}
