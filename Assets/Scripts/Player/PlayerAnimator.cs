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

    public void Update()
    {        
        if(_playerInventory.EquippedWeapon.LeftHandTargetIK != null)
            _leftHandIK.position = _playerInventory.EquippedWeapon.LeftHandTargetIK.position;

        if(_playerInventory.EquippedWeapon.RightHandTargetIK != null)
            _rightHandIK.position = _playerInventory.EquippedWeapon.RightHandTargetIK.position;

        _leftHandIK.rotation = _playerInventory.EquippedWeapon.LeftHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.LeftHandIKRotationOffset;
        _rightHandIK.rotation = _playerInventory.EquippedWeapon.RightHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.RightHandIKRotationOffset;


        _playerInventory.EquippedWeapon.SetPlayerMovementSpeed(_playerMovement.MoveVector.magnitude);
        _playerInventory.EquippedWeapon.SetPlayerAirborne(_playerMovement.CurrentState is PlayerFallingState);

        if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is RaycastWeaponReloadState) _playerAnim.SetInteger("Reloading", 2);
        else if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is not RaycastWeaponReloadState) _playerAnim.SetInteger("Reloading", 0);

        if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is BaseWeaponHolsterState) _playerAnim.SetTrigger("Hoslter");
    }
}
