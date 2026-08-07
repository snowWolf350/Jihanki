using System.Collections;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Press any key to start Screen")]
    [SerializeField] GameObject _startScreen;


    [Header("Camera Move animation")]
    [SerializeField]Animator _cameraAnimator;
    const string _MOVE = "move";

    [Header("Vending Machine Animation")]
    [SerializeField] Animator _vendingMachineAnimator;
    [SerializeField] GameObject _dustParticles;
    const string VENDING_MACHINE_ANIM = "_VendingMachineFall";

    private void Start()
    {
        GameInput.Instance.OnAnyKeyPressed += GameInput_OnAnyKeyPressed;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnAnyKeyPressed -= GameInput_OnAnyKeyPressed;
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
        _cameraAnimator.SetTrigger(_MOVE);

        //dont read any more random buttons
        GameInput.Instance.OnAnyKeyPressed -= GameInput_OnAnyKeyPressed;
    }

}
