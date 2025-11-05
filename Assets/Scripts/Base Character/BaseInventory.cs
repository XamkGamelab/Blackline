using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField]
    private Transform _weaponHolder;
    public Transform WeaponHolder => _weaponHolder;

    [SerializeField]
    private CharacterDataSheet _characterDataSheet;
    public CharacterDataSheet CharacterDataSheet => _characterDataSheet;

    private Dictionary<BaseWeapon, int> _ownedWeapons = new();
    public Dictionary<BaseWeapon, int> OwnedWeapons => _ownedWeapons;

    private Dictionary<BaseAmmoDataSheet, int> _ammoStorage = new();
    public Dictionary<BaseAmmoDataSheet, int> AmmoStorage => _ammoStorage;

    #region Adding & Dropping Weapons
    public void AddWeapon(BaseWeapon newWeapon)
    {
        if (!_ownedWeapons.ContainsKey(newWeapon))
        {
            _ownedWeapons.Add(newWeapon, 1);
            newWeapon.SetAmmoProvider(this);
        }
        else
        {
            if (newWeapon.WeaponData.CanAkimbo && _ownedWeapons[newWeapon] == 1)
            {
                _ownedWeapons[newWeapon]++;
                newWeapon.SetAmmoProvider(this);
            }
        }
    }

    public void DropWeapon(BaseWeapon dropWeapon)
    {
        dropWeapon.SetAmmoProvider(null);

        _ownedWeapons[dropWeapon]--;
        if (_ownedWeapons[dropWeapon] == 0) _ownedWeapons.Remove(dropWeapon);
    }
    #endregion

    #region Ammo
    public bool HasAmmo(BaseAmmoDataSheet ammo) => GetAmmoCount(ammo) > 0;

    public int GetAmmoCount(BaseAmmoDataSheet ammo)
    {
        return _ammoStorage.TryGetValue(ammo, out int count) ? count : 0;
    }

    public void AddAmmo(BaseAmmoDataSheet ammo, int amount)
    {
        // If the ammotype isn't in the storage, add the key & value pair and then add the amount. -Shad //
        if (!_ammoStorage.ContainsKey(ammo))
            _ammoStorage[ammo] = amount;

        _ammoStorage[ammo] += amount;
    }

    public void ConsumeAmmo(BaseAmmoDataSheet ammo, int amount)
    {
        if (_ammoStorage.TryGetValue(ammo, out int current))
        {
            current -= amount;

            if (current <= 0)
            {
                // Remove the ammotype from the storage completely if depleted. -Shad //
                _ammoStorage.Remove(ammo);
            }
        }
    }
    #endregion
}
