using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private Transform _leftHandIK;
    [SerializeField] 
    private Transform _rightHandIK;

    private void OnEnable()
    {
        _playerInventory.OnWeaponEquip += HandleOnEquipWeapon;
    }

    private void OnDisable()
    {
        _playerInventory.OnWeaponEquip -= HandleOnEquipWeapon;
    }

    [ContextMenu("Update Hands")]
    public void HandleOnEquipWeapon()
    {
        _leftHandIK.localRotation = Quaternion.identity * _playerInventory.EquippedWeapon.WeaponData.LeftHandIKRotationOffset;
        _rightHandIK.localRotation = Quaternion.identity * _playerInventory.EquippedWeapon.WeaponData.RightHandIKRotationOffset;
    }

    public void Update()
    {        
        if(_playerInventory.EquippedWeapon.LeftHandTargetIK != null)
            _leftHandIK.position = _playerInventory.EquippedWeapon.LeftHandTargetIK.position;

        if(_playerInventory.EquippedWeapon.RightHandTargetIK != null)
            _rightHandIK.position = _playerInventory.EquippedWeapon.RightHandTargetIK.position;
    }
}
