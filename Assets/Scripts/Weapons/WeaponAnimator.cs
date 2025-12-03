using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _weaponAnim;
    public Animator WeaponAnim => _weaponAnim;

    [SerializeField]
    private BaseWeapon _weapon;

    public void Update()
    {
        //_weaponAnim.SetFloat("PlayerSpeed", _weapon.PlayerMovementSpeed);
        //_weaponAnim.SetBool("Airborne", _weapon.PlayerAirborne);

        //if (_weapon.StateMachine.CurrentState == _weapon.HolsterState) _weaponAnim.SetTrigger("Holster");

        if (_weapon is RaycastWeapon raycastWeaponAiming) _weaponAnim.SetBool("Aiming", raycastWeaponAiming.Aiming);

        if (_weapon is RaycastWeapon raycastWeaponReloading && raycastWeaponReloading.StateMachine.CurrentState is RaycastWeaponReloadState) _weaponAnim.SetInteger("Reloading", 2);
        else if (_weapon is RaycastWeapon raycastWeaponNOTReloading && raycastWeaponNOTReloading.StateMachine.CurrentState is not RaycastWeaponReloadState) _weaponAnim.SetInteger("Reloading", 0);

        if (_weapon is MeleeWeapon meleeWeapon)
        {
            _weaponAnim.SetBool
                (
                "Swing", 
                meleeWeapon.StateMachine.CurrentState is MeleeWeaponLeftSwingState ||
                meleeWeapon.StateMachine.CurrentState is MeleeWeaponRightSwingState ||
                meleeWeapon.StateMachine.CurrentState is MeleeWeaponHeavySwingState
                );
        }
    }
}
