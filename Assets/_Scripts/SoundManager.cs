using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource _audioSource;

    [Header("Main Menu")]
    [SerializeField] AudioClip _slamSound;
    float _sfxVolume = 0.6f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayGroundSlamSound()
    {
        _audioSource.PlayOneShot(_slamSound,_sfxVolume);
    }
}
