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
        _weaponAnim.Play(_weapon.WeaponAction(), 0);
    }
}
