using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private Animator _playerAnim;

    [SerializeField]
    private Transform _leftHandIK;
    [SerializeField] 
    private Transform _rightHandIK;

    public void Update()
    {        
        if(_playerInventory.EquippedWeapon.LeftHandTargetIK != null)
            _leftHandIK.position = _playerInventory.EquippedWeapon.LeftHandTargetIK.position;

        if(_playerInventory.EquippedWeapon.RightHandTargetIK != null)
            _rightHandIK.position = _playerInventory.EquippedWeapon.RightHandTargetIK.position;

        _leftHandIK.rotation = _playerInventory.EquippedWeapon.LeftHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.LeftHandIKRotationOffset;
        _rightHandIK.rotation = _playerInventory.EquippedWeapon.RightHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.RightHandIKRotationOffset;

        _playerAnim.Play(_playerInventory.EquippedWeapon.PlayerAction(), 0);
    }
}
