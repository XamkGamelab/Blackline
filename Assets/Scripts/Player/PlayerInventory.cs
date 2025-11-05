using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    [SerializeField]
    private List<BaseWeapon> _weaponSlots;
    public List<BaseWeapon> WeaponSlots => _weaponSlots;

    // The current weapon the player is holding. -Shad //
    private BaseWeapon _equippedWeapon;
    public BaseWeapon EquippedWeapon => _equippedWeapon;

    public void SelectWeaponByKey(KeyCode keyCode)
    {
        WeaponCategory targetCategory = GlobalSettingsHolder.Instance.KeyToCategory[keyCode];

        for(int i = 0; i < _weaponSlots.Count; i++)
        {
            BaseWeapon index = _weaponSlots[i];

            if (index.WeaponData.WeaponCategory == targetCategory)
            {
                BaseWeaponDataSheet target = index.WeaponData;

                if (OwnedWeapons.ContainsKey(target))
                {
                    _equippedWeapon = index;
                }
            }
        }
    }
}
