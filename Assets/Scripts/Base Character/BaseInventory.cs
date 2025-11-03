using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField]
    private Transform _weaponHolder;
    [SerializeField]
    private CharacterDataSheet _characterDataSheet;

    private BaseWeapon _unarmed;
    private BaseWeapon _primaryWeapon;
    private BaseWeapon _secondaryWeapon;
    private List<BaseWeapon> _meleeWeapons = new List<BaseWeapon>();
    private List<BaseWeapon> _utilityWeapons = new List<BaseWeapon>();

    private Dictionary<BaseAmmoDataSheet, int> _ammoStorage = new();

    public Transform WeaponHolder => _weaponHolder;
    public CharacterDataSheet CharacterDataSheet => _characterDataSheet;

    public BaseWeapon Unarmed => _unarmed;
    public BaseWeapon PrimaryWeapon => _primaryWeapon;
    public BaseWeapon SecondaryWeapon => _secondaryWeapon;
    public List<BaseWeapon> MeleeWeapons => _meleeWeapons;
    public List<BaseWeapon> UtilityWeapons => _utilityWeapons;

    public Dictionary<BaseAmmoDataSheet, int> AmmoStorage => _ammoStorage;

    #region Setting Weapons
    public void SetPrimaryWeapon(BaseWeapon newWeapon)
    {
        // This is effectively just a filter to check if it really is a primary weapon. -Davoth //
        if (newWeapon.WeaponData.WeaponType != WeaponType.Primary)
        {
            print("That's not a primary. Moron."); 
            return;
        }
        else 
        {
            _primaryWeapon = newWeapon;
            _primaryWeapon.SetAmmoProvider(this);
        }
    }

    public void SetSecondaryWeapon(BaseWeapon newWeapon)
    {
        // This is effectively just a filter to check if it really is a secondary weapon. -Davoth //
        if (newWeapon.WeaponData.WeaponType != WeaponType.Secondary)
        {
            print("That's not a secondary. Idiot.");
            return;
        }
        else
        {
            _secondaryWeapon = newWeapon;
            _secondaryWeapon.SetAmmoProvider(this);
        }
    }

    public void SetMeleeWeapon(BaseWeapon newMelee, int currentMeleeIndex)
    {
        // This one is a bit more fucked. Please read carefully. -Shad //

        // First, the same filter with the previous ones. -Shad //
        if (newMelee.WeaponData.WeaponType != WeaponType.Melee)
        {
            print("That's not a Melee. Goofball.");
            return;
        }
        else
        {
            // If the melee weapon slots are not full, just add the new melee weapon as is. //
            // If the above is NOT true, then drop the *current* melee of that index and //
            // replace it with the new one. -Davoth //
            if (_meleeWeapons.Count != _characterDataSheet.MaxMeleeWeapons)
            {
                _meleeWeapons.Add(newMelee);
            }
            else
            {
                DropMeleeWeapon(_meleeWeapons[currentMeleeIndex]);
                _meleeWeapons.Add(newMelee);
            }
        }
    }

    public void SetUtilityWeapon(BaseWeapon newUtility, int currentUtilityIndex)
    {
        // Complete copy of the SetMeleeWeapon method, but for utility. -Shad //

        if (newUtility.WeaponData.WeaponType != WeaponType.Utility)
        {
            print("That's not a Utility. Bastard.");
            return;
        }
        else
        {
            if (_utilityWeapons.Count != _characterDataSheet.MaxUtilityWeapons)
            {
                _utilityWeapons.Add(newUtility);
            }
            else
            {
                DropUtilityWeapon(_utilityWeapons[currentUtilityIndex]);
                _utilityWeapons.Add(newUtility);
            }
        }
    }
    #endregion

    #region Dropping Weapons
    public void DropPrimaryWeapon(BaseWeapon dropWeapon)
    {
        _primaryWeapon.SetAmmoProvider(null);
    }

    public void DropSecondaryWeapon(BaseWeapon dropWeapon)
    {
        _secondaryWeapon.SetAmmoProvider(null);
    }
    
    public void DropMeleeWeapon(BaseWeapon dropWeapon)
    {

    }
    
    public void DropUtilityWeapon(BaseWeapon dropWeapon)
    {

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
