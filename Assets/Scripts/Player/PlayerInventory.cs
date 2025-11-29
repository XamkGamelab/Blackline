using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField]
    private Transform _weaponHolder;
    public Transform WeaponHolder => _weaponHolder;

    [SerializeField]
    private CharacterDataSheet _characterDataSheet;
    public CharacterDataSheet CharacterDataSheet => _characterDataSheet;

    [SerializeField]
    private WeaponAudio _weaponAudio;
    public WeaponAudio WeaponAudio => _weaponAudio;

    private Dictionary<BaseWeapon, int> _ownedWeapons = new();
    public Dictionary<BaseWeapon, int> OwnedWeapons => _ownedWeapons;

    private Dictionary<BaseAmmoDataSheet, int> _ammoStorage = new();
    public Dictionary<BaseAmmoDataSheet, int> AmmoStorage => _ammoStorage;

    // The current weapon the player is holding. -Shad //
    private BaseWeapon _equippedWeapon;
    public BaseWeapon EquippedWeapon => _equippedWeapon;
  
    public event Action OnWeaponEquip;

    private void Start() => Initialize();

    private void Initialize()
    {
        // Go through the Weaponholder transform's children to find out what weapons are //
        // already in the inventory. -Shad //
        for (int i = 0; i < WeaponHolder.childCount; i++)
        {
            BaseWeapon baseWeapon = WeaponHolder.GetChild(i).GetComponent<BaseWeapon>();
            AddWeapon(baseWeapon);
        }

        SelectWeaponByCategory(WeaponCategory.Medium);
    }

    #region Selecting & Equipping Weapons
    public void SelectWeaponByKey(KeyCode keyCode)
    {
        WeaponCategory targetCategory = GlobalSettingsHolder.Instance.KeyToCategory[keyCode];

        foreach(BaseWeapon key in OwnedWeapons.Keys)
        {
            if (key.WeaponData.WeaponCategory == targetCategory)
            {
                if (OwnedWeapons.ContainsKey(key))
                {
                    //_equippedWeapon = key;
                    EquipWeapon(key);
                    break;
                }
            }
        }
    }

    public void SelectWeaponByCategory(WeaponCategory weaponCategory)
    {
        foreach (BaseWeapon key in OwnedWeapons.Keys)
        {
            if (key.WeaponData.WeaponCategory == weaponCategory)
            {
                if (OwnedWeapons.ContainsKey(key))
                {
                    EquipWeapon(key);
                    break;
                }
            }
        }
    }

    public async void EquipWeapon(BaseWeapon newWeapon)
    {
        if(_equippedWeapon != null)
        {
            if (_equippedWeapon.WeaponData.WeaponName == newWeapon.WeaponData.WeaponName) { print("That's the same weapon."); return; }
            if (_equippedWeapon.StateMachine.CurrentState != _equippedWeapon.IdleState) return;

            _equippedWeapon.StateMachine.UpdateState(_equippedWeapon.HolsterState);

            while (!_equippedWeapon.ReadyToSwitch)
            {
                await Task.Yield();
            }

            _equippedWeapon.gameObject.SetActive(false);
        }

        _equippedWeapon = newWeapon;
        _equippedWeapon.Initialize();        
        _equippedWeapon.SetAmmoProvider(this);
        _equippedWeapon.SetAudioSource(_weaponAudio);

        _equippedWeapon.gameObject.SetActive(true);

        _equippedWeapon.StateMachine.UpdateState(_equippedWeapon.DrawState);
        
        OnWeaponEquip?.Invoke();
    }
    #endregion

    #region Adding & Dropping Weapons
    public void AddWeapon(BaseWeapon newWeapon)
    {
        if (!_ownedWeapons.ContainsKey(newWeapon))
        {
            _ownedWeapons.Add(newWeapon, 1);
        }
        else
        {
            if (newWeapon.WeaponData.CanAkimbo && _ownedWeapons[newWeapon] == 1)
            {
                _ownedWeapons[newWeapon]++;             
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
