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
    private float _moveX, _moveZ;
    private Vector3 _localMove, _targetPos;
    private void Update()
    {
        _lookRawX = Input.GetAxisRaw("Mouse X") * _playerInventory.EquippedWeapon.WeaponData.SwayAmountRot;
        _lookRawY = Input.GetAxisRaw("Mouse Y") * _playerInventory.EquippedWeapon.WeaponData.SwayAmountRot;

        rotationX = Quaternion.AngleAxis(-_lookRawY, Vector3.right);
        rotationY = Quaternion.AngleAxis(_lookRawX, Vector3.up);       

        targetRot = rotationX * rotationY;

        _localMove = transform.InverseTransformDirection(_playerMovement.MoveVector);
        _localMove = Vector3.ClampMagnitude(_localMove, _playerInventory.EquippedWeapon.WeaponData.SwayAmountPos);
        _moveX = _localMove.x * _playerInventory.EquippedWeapon.WeaponData.SwayAmountPos;
        _moveZ = _localMove.z * _playerInventory.EquippedWeapon.WeaponData.SwayAmountPos;

        _targetPos = new(_moveX, 0f, _moveZ);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessRot * Time.deltaTime);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _playerInventory.EquippedWeapon.WeaponData.SwaySmoothnessPos * Time.deltaTime);
    }
}
