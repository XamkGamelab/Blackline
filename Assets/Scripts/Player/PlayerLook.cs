using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerInventory _playerInventory;
    [SerializeField]
    private Transform _cameraPivot;
    [SerializeField]
    private Transform _swayPivot;
    [SerializeField]
    private Camera _cam;

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

    private RaycastWeapon _raycastWeapon;
    private void Start() => _raycastWeapon = (RaycastWeapon)_playerInventory.EquippedWeapon;

    private void OnEnable()
    {
        _raycastWeapon.OnWeaponZoom += HandleWeaponZoom;
    }

    private void OnDisable()
    {
        _raycastWeapon.OnWeaponZoom -= HandleWeaponZoom;
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
        _cameraPivot.transform.localRotation = Quaternion.Euler(_camLookX, 0f, 0f);
        transform.Rotate(transform.up * _camLookY);
    }

    private float _targetFOV, _refSmooth;
    private void Cinematics()
    {
        Mathf.SmoothDamp(_cam.fieldOfView, _targetFOV, ref _refSmooth, 0.25f);

        if(_playerMovement.CurrentState == _playerMovement.CrouchState)
        {
            _cameraPivot.localPosition = Vector3.Lerp(_cameraPivot.localPosition, new(0f, _playerDataSheet.CameraCrouchPosY, 0f), 5f * Time.deltaTime);
        }
        else
        {
            _cameraPivot.localPosition = Vector3.Lerp(_cameraPivot.localPosition, new(0f, _playerDataSheet.CameraDefaultPosY, 0f), 5f * Time.deltaTime);
        }
    }

    private void HandleWeaponZoom()
    {
        print("Zoom");

        _targetFOV = _raycastWeapon.Aiming ? _playerDataSheet.WalkingCameraFOV / _raycastWeapon.DataSheet.AimZoom : _playerDataSheet.WalkingCameraFOV;
    }
}
