using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Player _player;

    Animator _animator;

    const string _IS_WALKING = "isWalking";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _animator.SetBool(_IS_WALKING,_player.IsPlayerWalking());
    }
}
