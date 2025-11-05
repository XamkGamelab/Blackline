using UnityEngine;

public class PlayerInventory : BaseInventory
{
    // The current weapon the player is holding. -Shad //
    private BaseWeapon _equippedWeapon;
    public BaseWeapon EquippedWeapon => _equippedWeapon;

    private void Start() => Initialize();

    private void Initialize()
    {
        // Go through the Weaponholder transform's children to find out what weapons are //
        // already in the inventory. -Shad //
        for (int i = 0; i < WeaponHolder.childCount; i++)
        {
            BaseWeapon baseWeapon = WeaponHolder.GetChild(i).GetComponent<BaseWeapon>();
            AddWeapon(baseWeapon);
            SelectWeaponByCategory(baseWeapon.WeaponData.WeaponCategory);
        }
    }

    public void SelectWeaponByKey(KeyCode keyCode)
    {
        WeaponCategory targetCategory = GlobalSettingsHolder.Instance.KeyToCategory[keyCode];

        foreach(BaseWeapon key in OwnedWeapons.Keys)
        {
            if (key.WeaponData.WeaponCategory == targetCategory)
            {
                if (OwnedWeapons.ContainsKey(key))
                {
                    _equippedWeapon = key;
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
                    _equippedWeapon = key;
                    break;
                }
            }
        }
    }

    public void EquipWeapon(BaseWeapon newWeapon)
    {
        if (_equippedWeapon.StateMachine.CurrentState != _equippedWeapon.IdleState) return;
        if (_equippedWeapon.WeaponData == newWeapon.WeaponData) return;

        _equippedWeapon.gameObject.SetActive(false);
        _equippedWeapon = newWeapon;
        _equippedWeapon.gameObject.SetActive(true);
    }
}
