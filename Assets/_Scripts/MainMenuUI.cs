using System.Collections;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Press any key to start Screen")]
    [SerializeField] GameObject _startScreen;


    [Header("Camera Transforms")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _cameraStartTransform;
    [SerializeField] Transform _cameraEndTransform;

    [Header("Vending Machine Animation")]
    [SerializeField] Animator _vendingMachineAnimator;
    [SerializeField] GameObject _dustParticles;
    const string VENDING_MACHINE_ANIM = "_VendingMachineFall";

    private void Start()
    {
        GameInput.Instance.OnAnyKeyPressed += GameInput_OnAnyKeyPressed;
    }

    private void GameInput_OnAnyKeyPressed(object sender, System.EventArgs e)
    {
        // hide the press any button screen
        _startScreen.SetActive(false);

        //make the vending machine fall
        _vendingMachineAnimator.Play(VENDING_MACHINE_ANIM);
        _dustParticles.SetActive(true);

        SoundManager.Instance.PlayGroundSlamSound();

        //move the camera
        StartCoroutine(MoveCamera());

        //dont read any more random buttons
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
