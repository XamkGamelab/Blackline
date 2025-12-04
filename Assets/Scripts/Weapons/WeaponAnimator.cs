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
        if(_weapon is RaycastWeapon)
        {
            _weaponAnim.SetBool("Aiming", _weapon.Aiming);

            if (_weapon.StateMachine.CurrentState is RaycastWeaponEmergencyReloadState) _weaponAnim.SetInteger("Reloading", 2);
            else if (_weapon.StateMachine.CurrentState is RaycastWeaponTacticalReloadState) _weaponAnim.SetInteger("Reloading", 1);
            else if (_weapon.StateMachine.CurrentState is not RaycastWeaponTacticalReloadState && _weapon.StateMachine.CurrentState is not RaycastWeaponEmergencyReloadState) _weaponAnim.SetInteger("Reloading", 0);
        }

        if(_weapon is MeleeWeapon meleeWeapon)
        {
            _weaponAnim.SetInteger("SwingIndex", meleeWeapon.CurrentAnimIndex);
        }
    }
}
