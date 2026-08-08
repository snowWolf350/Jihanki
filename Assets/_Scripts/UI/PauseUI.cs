using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    [SerializeField] Button _continueButton;
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _quitButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetGameStateToPlaying();
            Hide();
        });
        _mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadMainMenu();
        });
        _quitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        
        });

    }

    private void Start()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePaused())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
       gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
