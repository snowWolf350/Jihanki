using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject _startScreen;

    private void Start()
    {
        GameInput.Instance.OnAnyKeyPressed += GameInput_OnAnyKeyPressed;
    }

    private void GameInput_OnAnyKeyPressed(object sender, System.EventArgs e)
    {
        _startScreen.SetActive(false);
        GameInput.Instance.OnAnyKeyPressed -= GameInput_OnAnyKeyPressed;
    }
}
