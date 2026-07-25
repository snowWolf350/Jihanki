using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    CinemachineCamera _cineCam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _cineCam = GetComponent<CinemachineCamera>();
    }

    public void SetTargetTo(Transform transform)
    {
        _cineCam.Target.TrackingTarget = transform;
    }
}
