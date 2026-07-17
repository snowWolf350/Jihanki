using UnityEngine;

public class Player : MonoBehaviour
{
    float _playerSpeed = 5;

    CharacterController _characterController;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        _characterController.Move(moveDir * _playerSpeed * Time.deltaTime);
    }
}
