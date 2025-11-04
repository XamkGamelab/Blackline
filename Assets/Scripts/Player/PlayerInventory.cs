using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    [SerializeField]
    private List<BaseWeaponDataSheet> _weaponSlots;

    public void SelectWeaponByNumber(int index)
    {
        if (index < 0 || index >= _weaponSlots.Count) return;

        BaseWeaponDataSheet target = _weaponSlots[index];

        if (OwnedWeapons.ContainsKey(target))
        {
            // Equip the weapon logic here please. -Shad //
        }
    }
}
