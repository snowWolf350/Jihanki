using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    [SerializeField] Button _continueButton;
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _quitButton;

    bool _isPaused;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            _isPaused = false;
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
        GameInput.Instance.OnEscapePressed += Instance_OnEscapePressed;
        Hide();
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnEscapePressed -= Instance_OnEscapePressed;
    }
    private void Instance_OnEscapePressed(object sender, System.EventArgs e)
    {
        if (_isPaused)
        {
            //game is paused time to unpause
            Hide();
        }
        else
        {
            //game is not paused have to pause
            Show();
        }
        _isPaused = !_isPaused;
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
