using UnityEngine;

public abstract class BaseWeaponDataSheet : ScriptableObject
{
    [Header("Base Weapon Data")]
    [SerializeField]
    private string _weaponName;
    [SerializeField]
    private WeaponType _weaponType;

    public string WeaponName => _weaponName;
    public WeaponType WeaponType => _weaponType;
}

public enum WeaponType
{
    Primary, Secondary, Melee, Utility
}
