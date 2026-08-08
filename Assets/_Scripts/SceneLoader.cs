using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] Animator _levelTransitionAnim;

    public static event EventHandler OnSceneChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnSceneChanged?.Invoke(this, EventArgs.Empty);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadNewScene(1));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadNewScene(0));
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadNewScene(int buildIndex)
    {
        Time.timeScale = 1;
        _levelTransitionAnim.SetTrigger("start");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(buildIndex);
    }

    public static int GetBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
