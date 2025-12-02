using UnityEngine;

public class SwayEffect : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerLook _playerLook;
    [SerializeField]
    private PlayerInventory _playerInventory;

    private float _lookRawX, _lookRawY;
    private Quaternion rotationX, rotationY, targetRot;
    private Vector3 _localMove, _currentPos, _targetPos;
    private float aimingMultiplier;
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
        aimingMultiplier = _playerInventory.EquippedWeapon.Aiming ? _playerInventory.EquippedWeapon.WeaponData.AimingSwayMultiplier : 1f;

        _localMove = transform.InverseTransformDirection(_playerMovement.MoveVector);
        _localMove = Vector3.ClampMagnitude(_localMove, _playerMovement.PlayerData.WalkSpeed);

        _localMove *= _playerInventory.EquippedWeapon.WeaponData.SwayAmountPos;

        _targetPos = Vector3.Lerp(_targetPos, -_localMove * aimingMultiplier, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessPos * Time.deltaTime);
        _currentPos = Vector3.Slerp(_currentPos, _targetPos, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessPos * Time.deltaTime);

        transform.localPosition = _currentPos;
    }
}
