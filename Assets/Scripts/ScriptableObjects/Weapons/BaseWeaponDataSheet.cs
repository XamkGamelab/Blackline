using UnityEngine;

public abstract class BaseWeaponDataSheet : ScriptableObject
{
    [Header("Base Weapon Data")]
    [SerializeField]
    private WeaponCategory _weaponCategory;
    public WeaponCategory WeaponCategory => _weaponCategory;
    [SerializeField]
    private string _weaponName;
    public string WeaponName => _weaponName;
    [SerializeField]
    private float _drawTime;
    public float DrawTime => _drawTime;
    [SerializeField]
    private float _holsterTime;
    public float HolsterTime => _holsterTime;
    [SerializeField]
    private bool _canAkimbo;
    public bool CanAkimbo => _canAkimbo;
}

public enum WeaponCategory
{
    Melee,
    Light,
    Shell,
    Medium,
    Heavy,
    Plasma,
    Rocket,
    Utility,
    Throwable,
}
