using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerInventory _playerInventory;
    [SerializeField]
    private PlayerHealth _playerHealth;

    [SerializeField]
    private Animator _playerAnim;

    [SerializeField]
    private Transform _leftHandIK;
    [SerializeField] 
    private Transform _rightHandIK;

    private void OnEnable()
    {
        _playerInventory.WeaponEquipEvent += OnWeaponEquipped;
        _playerInventory.WeaponUnequipEvent += OnWeaponUnequipped;
        _playerHealth.DamageTakenEvent += OnDamageTaken;
    }

    private void OnDisable()
    {
        _playerInventory.WeaponEquipEvent -= OnWeaponEquipped;
        _playerInventory.WeaponUnequipEvent += OnWeaponUnequipped;
        _playerHealth.DamageTakenEvent -= OnDamageTaken;
    }

    private void Start()
    {
        _playerAnim.Play($"ViktorKainFP_Rig|ViktorFP_{_playerInventory.EquippedWeapon.WeaponData.WeaponName}_Draw");
    }

    public void Update()
    {        
        if(_playerInventory.EquippedWeapon.LeftHandTargetIK != null)
            _leftHandIK.position = _playerInventory.EquippedWeapon.LeftHandTargetIK.position;

        if(_playerInventory.EquippedWeapon.RightHandTargetIK != null)
            _rightHandIK.position = _playerInventory.EquippedWeapon.RightHandTargetIK.position;

        _leftHandIK.rotation = _playerInventory.EquippedWeapon.LeftHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.LeftHandIKRotationOffset;
        _rightHandIK.rotation = _playerInventory.EquippedWeapon.RightHandTargetIK.rotation * _playerInventory.EquippedWeapon.WeaponData.RightHandIKRotationOffset;

        _playerInventory.EquippedWeapon.WeaponAnimator.SetFloat("PlayerSpeed", _playerMovement.MoveVector.magnitude);
        _playerInventory.EquippedWeapon.WeaponAnimator.SetBool("Airborne", _playerMovement.CurrentState is PlayerFallingState);

        if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is RaycastWeaponEmergencyReloadState) _playerAnim.SetInteger("Reloading", 2);
        else if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is RaycastWeaponTacticalReloadState) _playerAnim.SetInteger("Reloading", 1);
        else if (_playerInventory.EquippedWeapon.StateMachine.CurrentState is not RaycastWeaponTacticalReloadState && _playerInventory.EquippedWeapon.StateMachine.CurrentState is not RaycastWeaponEmergencyReloadState) _playerAnim.SetInteger("Reloading", 0);
    }

    private void OnWeaponEquipped()
    {
        _playerAnim.Play($"ViktorKainFP_Rig|ViktorFP_{_playerInventory.EquippedWeapon.WeaponData.WeaponName}_Draw");
    }

    private void OnWeaponUnequipped()
    {
        _playerAnim.SetTrigger("Holster");
        _playerAnim.ResetTrigger("Holster");

        _playerInventory.EquippedWeapon.WeaponAnimator.SetTrigger("Holster");
    }

    private void OnDamageTaken()
    {
        if(_playerHealth.CurrentHealth <= 0f)
        {
            _playerAnim.Play($"ViktorKainFP_Rig|ViktorFP_{_playerInventory.EquippedWeapon.WeaponData.WeaponName}_Holster");
            _playerInventory.EquippedWeapon.WeaponAnimator.SetTrigger("Holster");
        }        
    }
}
