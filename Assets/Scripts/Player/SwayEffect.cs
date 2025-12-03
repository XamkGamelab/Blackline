using UnityEngine;

public class SwayEffect : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerLook _playerLook;
    [SerializeField]
    private PlayerInventory _playerInventory;
    [SerializeField]
    private Transform _weaponHolder;

    private float _lookRawX, _lookRawY;
    private Quaternion rotationX, rotationY, targetRot;
    private Vector3 _localMove, _currentPos, _targetPos;
    private float _aimingMultiplier;
    private float _currentSlideAngle, _targetSlideAngle;
    private void Update()
    {
        // Rotation sway. -Shad //
        _lookRawX = Input.GetAxisRaw("Mouse X") * _playerInventory.EquippedWeapon.WeaponData.SwayAmountRot;
        _lookRawY = Input.GetAxisRaw("Mouse Y") * _playerInventory.EquippedWeapon.WeaponData.SwayAmountRot;

        rotationX = Quaternion.AngleAxis(-_lookRawY, Vector3.right);
        rotationY = Quaternion.AngleAxis(_lookRawX, Vector3.up);       

        targetRot = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessRot * Time.deltaTime);

        // Position sway. -Shad //
        _aimingMultiplier = _playerInventory.EquippedWeapon.Aiming ? _playerInventory.EquippedWeapon.WeaponData.AimingSwayMultiplier : 1f;

        _localMove = transform.InverseTransformDirection(_playerMovement.MoveVector);
        _localMove = Vector3.ClampMagnitude(_localMove, _playerMovement.PlayerData.WalkSpeed);

        _localMove *= _playerInventory.EquippedWeapon.WeaponData.SwayAmountPos;

        _targetPos = Vector3.Lerp(_targetPos, -_localMove * _aimingMultiplier, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessPos * Time.deltaTime);
        _currentPos = Vector3.Slerp(_currentPos, _targetPos, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessPos * Time.deltaTime);

        transform.localPosition = _currentPos;

        // Weapon holder angle based on sliding. -Shad //
        _targetSlideAngle = _playerMovement.CurrentState == _playerMovement.SlideState ? _playerInventory.EquippedWeapon.WeaponData.SlideWeaponAngle : 0f;
        _currentSlideAngle = Mathf.Lerp(_currentSlideAngle, _targetSlideAngle, _playerMovement.PlayerData.CrouchTransitionSpeed * Time.deltaTime);

        _weaponHolder.localRotation = Quaternion.Euler(Vector3.forward * _currentSlideAngle);
    }
}
