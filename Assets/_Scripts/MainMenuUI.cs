using System.Collections;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject _startScreen;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _cameraStartTransform;
    [SerializeField] Transform _cameraEndTransform;

    private void Start()
    {
        GameInput.Instance.OnAnyKeyPressed += GameInput_OnAnyKeyPressed;
    }

    private void GameInput_OnAnyKeyPressed(object sender, System.EventArgs e)
    {
        _startScreen.SetActive(false);
        StartCoroutine(MoveCamera());
        GameInput.Instance.OnAnyKeyPressed -= GameInput_OnAnyKeyPressed;
    }

    IEnumerator MoveCamera()
    {
        float t = 0;
        while (t <= 1)
        {
            _cameraTransform.position = Vector3.Lerp(_cameraStartTransform.position, _cameraEndTransform.position, t);
            _cameraTransform.forward = Vector3.Slerp(_cameraStartTransform.forward, _cameraEndTransform.forward, t);
            t += Time.deltaTime;
            yield return null;
        }
        _cameraTransform = _cameraEndTransform;
    }
}
