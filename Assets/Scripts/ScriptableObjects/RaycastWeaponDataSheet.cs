using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon Data Sheet", menuName = "Data Sheets/Weapons/Create Raycast Weapon Data Sheet")]
public class RaycastWeaponDataSheet : BaseWeaponDataSheet
{
    [Header("Weapon Features")]
    [SerializeField]
    private float _shotCooldownFromHip;
    [SerializeField]
    private float _shotCooldownAimed;
    [SerializeField]
    private FiringMode _firingModes;
    [SerializeField]
    private int _burstCount;

    [Header("Ballistics")]
    [SerializeField]
    private float _projectileMaxRange;
    [SerializeField]
    private float _projectileSpreadX;
    [SerializeField]
    private float _projectileSpreadY;
    [SerializeField]
    private float _accuracySpreadMultiplier;

    [Header("Aiming")]
    [SerializeField]
    private float _aimZoom;

    [Header("Ammo")]
    [SerializeField]
    private int _maxAmmoInWeapon;
    [SerializeField]
    private List<BaseAmmoDataSheet> _compatibleAmmo;

    // This effectively sets the values as read only, but available from other classes. -Shad //
    public float ShotCooldownFromHip => _shotCooldownFromHip;
    public float ShotCooldownAimed => _shotCooldownAimed;
    public FiringMode FiringModes => _firingModes;
    public int BurstCount => _burstCount;

    public float ProjectileMaxRange => _projectileMaxRange;
    public float ProjectileSpreadX => _projectileSpreadX;
    public float ProjectileSpreadY => _projectileSpreadY;
    public float AccuracySpreadMultiplier => _accuracySpreadMultiplier;

    public float AimZoom => _aimZoom;

    public int MaxAmmoInWeapon => _maxAmmoInWeapon;
    public List<BaseAmmoDataSheet> CompatibleAmmo => _compatibleAmmo;
}

[Flags]
public enum FiringMode
{
    None = 0,
    Single = 1 << 0,
    Burst = 1 << 1,
    Automatic = 1 << 2,
}
