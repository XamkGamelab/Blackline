using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private Animator _playerAnim;

    [SerializeField]
    private Transform _leftHandIK;
    [SerializeField] 
    private Transform _rightHandIK;

    private string _animationClipCache;

    public void Update()
    {        
        if(_playerInventory.EquippedWeapon.LeftHandTargetIK != null)
            _leftHandIK.position = _playerInventory.EquippedWeapon.LeftHandTargetIK.position;

        if(_playerInventory.EquippedWeapon.RightHandTargetIK != null)
            _rightHandIK.position = _playerInventory.EquippedWeapon.RightHandTargetIK.position;

        _leftHandIK.rotation = _playerInventory.EquippedWeapon.LeftHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.LeftHandIKRotationOffset;
        _rightHandIK.rotation = _playerInventory.EquippedWeapon.RightHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.RightHandIKRotationOffset;

        if(_animationClipCache != _playerInventory.EquippedWeapon.PlayerAnimAction())
        {
            _playerAnim.PlaySmooth(Animator.StringToHash(_playerInventory.EquippedWeapon.PlayerAnimAction()), 0.05f);
            _animationClipCache = _playerInventory.EquippedWeapon.PlayerAnimAction();
        }

        _playerInventory.EquippedWeapon.SetPlayerMovementContext(_playerMovement.WeaponMovementContext());
    }
}
