using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource _audioSource;

    [Header("Main Menu")]
    [SerializeField] AudioClip _slamSound;
    [SerializeField] AudioClip _coinInsertSound;
    [SerializeField] AudioClip _uiHoverSound;
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
    public void PlayCoinInsertSound()
    {
        _audioSource.PlayOneShot(_coinInsertSound, _sfxVolume);
    }
    public void PlayUiHoverSound()
    {
        _audioSource.PlayOneShot(_uiHoverSound, _sfxVolume);
    }
}
