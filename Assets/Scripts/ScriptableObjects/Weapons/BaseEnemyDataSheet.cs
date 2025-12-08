using UnityEngine;

public abstract class BaseEnemyDataSheet : ScriptableObject
{
    [Header("Base Enemy Data")]
    [SerializeField]
    private string _manufacturer;
    public string Manufacturer => _manufacturer;
    [SerializeField]
    private string _enemyName;
    public string EnemyName => _enemyName;
    [SerializeField]
    private float _maxHealth;
    public float MaxHealth => _maxHealth;
    [SerializeField]
    private float _maxArmor;
    public float MaxArmor => _maxArmor;
    [SerializeField]
    private BaseWeaponDataSheet _equippedWeapon;
    public BaseWeaponDataSheet EquippedWeapon => _equippedWeapon;
    [SerializeField]
    private BaseAmmoDataSheet _equippedAmmo;
    public BaseAmmoDataSheet EquippedAmmo => _equippedAmmo;
}
