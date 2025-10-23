using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Camera _playerCam;

    // Zenject dependency injection. //  
    private PlayerDataSheet _playerDataSheet;

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        _playerDataSheet = playerDataSheet;
    }

    // Lock the cursor and hide it. //
    private void Awake() => Initialize();

    private void Update()
    {
        CameraLogic();
        Cinematics();
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _targetFOV = _playerDataSheet.WalkingCameraFOV;
    }

    private float _camLookX, _camLookY;    
    private void CameraLogic()
    {
        // Limit the up and down axis to -90 & 90 degrees. //
        _camLookX = Mathf.Clamp(_camLookX, -90f, 90f);

        // Cam Look X is actually Mouse Y, and Cam Look Y is actually Mouse X. Pretty silly right? //
        _camLookX -= Input.GetAxis("Mouse Y") * GlobalSettingsHolder.Instance.PlayerSettingsData.MouseSensitivity * Time.deltaTime;
        _camLookY = Input.GetAxis("Mouse X") * GlobalSettingsHolder.Instance.PlayerSettingsData.MouseSensitivity * Time.deltaTime;

        // Rotate only the camera locally with _camLookX, and the whole player with _camLookY. //
        _playerCam.transform.localRotation = Quaternion.Euler(_camLookX, 0f, 0f);
        transform.Rotate(transform.up * _camLookY);
    }

    private float _targetFOV, _refSmooth;
    private void Cinematics()
    {
        // Some kind of smooth transition between the FOVs would be necessary. Right now, fucking horrible. -Davoth //
        _targetFOV = Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.RunKey) ?
            Mathf.SmoothDamp(_targetFOV, _playerDataSheet.RunningCameraFOV, ref _refSmooth, 0.5f) :
            Mathf.SmoothDamp(_targetFOV, _playerDataSheet.WalkingCameraFOV, ref _refSmooth, 0.5f);

        _playerCam.fieldOfView = _targetFOV;
    }
}
