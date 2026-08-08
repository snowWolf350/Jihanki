using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("AudioSources")]
    [SerializeField] AudioSource _musicAudioSource;
    [SerializeField] AudioSource _sfxAudioSource;

    [Header("Main Menu SFX")]
    [SerializeField] AudioClip _slamSound;
    [SerializeField] AudioClip _coinInsertSound;
    [SerializeField] AudioClip _uiHoverSound;

    [Header("In Game SFX")]
    [SerializeField] AudioClip _uiPopSound;
    [SerializeField] AudioClip _heavyClick;
    [SerializeField] AudioClip _errorSound;

    [Header("Music")]
    [SerializeField] AudioClip _mainMenuMusic;
    [SerializeField] AudioClip _InGameMusic;

    float _inGameMusicVolume = 0.2f;
    float _mainMenuMusicVolume = 0.4f;
    float _sfxVolume = 0.8f;
    float _pitchVariationMax = .2f;

    Coroutine _fadeInCoroutine;

    private void Awake()
    {
        SceneLoader.DontDestroyOnLoad(this);
        Instance = this;

        PartObject.OnParentChanged += PartObject_OnParentChanged;
        SceneLoader.OnSceneChanged += SceneLoader_OnSceneChanged;
        GameManager.OnMenuOpened += GameManager_OnMenuOpened;
    }


    private void OnDestroy()
    {
        PartObject.OnParentChanged -= PartObject_OnParentChanged;
        SceneLoader.OnSceneChanged -= SceneLoader_OnSceneChanged;
        GameManager.OnMenuOpened -= GameManager_OnMenuOpened;
    }
    private void GameManager_OnMenuOpened(object sender, System.EventArgs e)
    {
        PlaySfxOneShot(_heavyClick);
    }
    private void PartObject_OnParentChanged(object sender, System.EventArgs e)
    {
        PlaySfxOneShot(_uiHoverSound);
    }

    private void SceneLoader_OnSceneChanged(object sender, System.EventArgs e)
    {
        if (_fadeInCoroutine != null)
        {
            StopCoroutine(_fadeInCoroutine);
        }


        if (SceneLoader.GetBuildIndex() == 2)
        {
            //load in game music 
            _musicAudioSource.clip = _InGameMusic;
            _fadeInCoroutine = StartCoroutine(FadeInMusic(_inGameMusicVolume));
            _musicAudioSource.Play();
            return;
        }
        else if (SceneLoader.GetBuildIndex() == 0)
        {
            _musicAudioSource.clip = _mainMenuMusic;
            _fadeInCoroutine = StartCoroutine(FadeInMusic(_mainMenuMusicVolume));
            _musicAudioSource.Play();
            return;
        }
    }

    public void PlayGroundSlamSound()
    {
        PlaySfxOneShot(_slamSound);
    }
    public void PlayCoinInsertSound()
    {
        PlaySfxOneShot(_coinInsertSound);
    }
    public void PlayUiHoverSound()
    {
        PlaySfxOneShot(_uiHoverSound);
    }
    public void PlayUIPopSound()
    {
        PlaySfxOneShot(_uiPopSound);
    }
    public void PlayHeavyClickSound()
    {
        PlaySfxOneShot(_heavyClick);
    }
    public void PlayErrorSound()
    {
        PlaySfxOneShot(_errorSound);
    }

    void PlaySfxOneShot(AudioClip audioClip)
    {
        _sfxAudioSource.pitch = 1 + Random.Range(-_pitchVariationMax, _pitchVariationMax);
        _sfxAudioSource.PlayOneShot(audioClip, _sfxVolume);
    }

    IEnumerator FadeInMusic(float volume)
    {
        float t = 0;
        while (t < 1)
        {
            _musicAudioSource.volume = Mathf.Lerp(0,volume, t);
            t += Time.deltaTime;
            yield return null;
        }

        _musicAudioSource.volume = volume;
        _fadeInCoroutine = null;
    }
}
