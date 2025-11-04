using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField]
    private Transform _weaponHolder;

    [SerializeField]
    private CharacterDataSheet _characterDataSheet;
    public CharacterDataSheet CharacterDataSheet => _characterDataSheet;

    private Dictionary<BaseWeaponDataSheet, int> _ownedWeapons = new();
    public Dictionary<BaseWeaponDataSheet, int> OwnedWeapons => _ownedWeapons;

    private Dictionary<BaseAmmoDataSheet, int> _ammoStorage = new();
    public Dictionary<BaseAmmoDataSheet, int> AmmoStorage => _ammoStorage;

    private BaseWeapon _unarmed;
    private BaseWeapon _primaryWeapon;
    private BaseWeapon _secondaryWeapon;
    private List<BaseWeapon> _meleeWeapons = new List<BaseWeapon>();
    private List<BaseWeapon> _utilityWeapons = new List<BaseWeapon>();

    public BaseWeapon Unarmed => _unarmed;
    public BaseWeapon PrimaryWeapon => _primaryWeapon;
    public BaseWeapon SecondaryWeapon => _secondaryWeapon;
    public List<BaseWeapon> MeleeWeapons => _meleeWeapons;
    public List<BaseWeapon> UtilityWeapons => _utilityWeapons;

    private void Awake() => Initialize();

    private void Initialize()
    {
        // Set the maximum capacity of these two list according to the player data sheet. -Shad //
        MeleeWeapons.Capacity = CharacterDataSheet.MaxMeleeWeapons;
        UtilityWeapons.Capacity = CharacterDataSheet.MaxUtilityWeapons;

        // Go through the Weaponholder transform's children to find out what weapons are //
        // already in the inventory. -Shad //
        for (int i = 0; i < _weaponHolder.childCount; i++)
        {
            // Setup an index. We know for sure that there can be only weapons in these objects. -Shad //
            // WeaponType _indexWeaponType = _weaponHolder.GetChild(i).GetComponent<BaseWeapon>().WeaponData.WeaponType;

            // if (_indexWeaponType == WeaponType.Primary) AddWeapon(_weaponHolder.GetChild(i).GetComponent<BaseWeapon>());
            // if (_indexWeaponType == WeaponType.Secondary) AddWeapon(_weaponHolder.GetChild(i).GetComponent<BaseWeapon>());

            // Since these two WeaponTypes can be carried in different amounts, they will have to be //
            // assigned in a different way. -Shad //
            // if (_index.WeaponData.WeaponType == WeaponType.Melee) _primaryWeapon = _index;
            // if (_index.WeaponData.WeaponType == WeaponType.Primary) _primaryWeapon = _index;
        }
    }

    #region Setting Weapons
    public void AddWeapon(BaseWeaponDataSheet newWeapon)
    {
        if (!_ownedWeapons.ContainsKey(newWeapon))
        {
            _ownedWeapons.Add(newWeapon, 1);
            //newWeapon.SetAmmoProvider(this);
        }
        else
        {
            if (newWeapon.CanAkimbo) _ownedWeapons[newWeapon]++;
        }
    }
    #endregion

    #region Dropping Weapons
    public void DropWeapon(BaseWeaponDataSheet dropWeapon)
    {
        //dropWeapon.SetAmmoProvider(null);

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
