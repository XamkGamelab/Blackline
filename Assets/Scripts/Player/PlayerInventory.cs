public class PlayerInventory : BaseInventory
{
    private void Awake() => Initialize();

    private void Initialize()
    {
        // Set the maximum capacity of these two list according to the player data sheet. -Davoth //
        MeleeWeapons.Capacity = CharacterDataSheet.MaxMeleeWeapons;
        UtilityWeapons.Capacity = CharacterDataSheet.MaxUtilityWeapons;

        // Go through the Weaponholder transform's children to find out what weapons are //
        // already in the inventory. -Davoth //
        for(int i = 0; i < WeaponHolder.childCount; i++)
        {
            // Setup an index. We know for sure that there can be only weapons in these objects. -Davoth //
            WeaponType _indexWeaponType = WeaponHolder.GetChild(i).GetComponent<BaseWeapon>().WeaponData.WeaponType;

            if (_indexWeaponType == WeaponType.Primary) SetPrimaryWeapon(WeaponHolder.GetChild(i).GetComponent<BaseWeapon>());
            if (_indexWeaponType == WeaponType.Secondary) SetSecondaryWeapon(WeaponHolder.GetChild(i).GetComponent<BaseWeapon>());
            
            // Since these two WeaponTypes can be carried in different amounts, they will have to be //
            // assigned in a different way. -Davoth //
            // if (_index.WeaponData.WeaponType == WeaponType.Melee) _primaryWeapon = _index;
            // if (_index.WeaponData.WeaponType == WeaponType.Primary) _primaryWeapon = _index;
        }
    }
}
