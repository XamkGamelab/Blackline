using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _weaponAnim;
    public Animator WeaponAnim => _weaponAnim;

    [SerializeField]
    private BaseWeapon _weapon;

    private string _animationClipCache;

    public void Update()
    {
        if(_animationClipCache != _weapon.WeaponAnimAction())
        {
            _weaponAnim.PlaySmooth(Animator.StringToHash(_weapon.WeaponAnimAction()), 0.05f);
            _animationClipCache = _weapon.WeaponAnimAction();
        }
    }
}
