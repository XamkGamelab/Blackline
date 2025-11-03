using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private BaseWeaponDataSheet _baseWeaponDataSheet;
    public BaseWeaponDataSheet WeaponData => _baseWeaponDataSheet;

    private IAmmoProvider _ammoProvider;
    public IAmmoProvider AmmoProvider => _ammoProvider;

    // Inventory scripts can set the provider with this method. -Shad //
    public void SetAmmoProvider(IAmmoProvider ammoProvider) => _ammoProvider = ammoProvider;

    // For things like shooting a weapon, or swinging an axe. -Shad //
    public virtual void PrimaryFunction() { }

    // For things like aiming a weapon, or perhaps throwing an axe. -Shad //
    public virtual void SecondaryFunction() { }

    // For things like cycling firing modes. -Shad //
    public virtual void ThirdFunction() { }

    public virtual void HandleUpdate() { }
}
