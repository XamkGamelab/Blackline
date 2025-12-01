using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon Data Sheet", menuName = "Data Sheets/Weapons/Create Raycast Weapon Data Sheet")]
public class RaycastWeaponDataSheet : BaseWeaponDataSheet
{
    [Header("Weapon Features")]
    [SerializeField]
    private float _shotCooldownFromHip;
    public float ShotCooldownFromHip => _shotCooldownFromHip;
    [SerializeField]
    private float _shotCooldownAimed;
    public float ShotCooldownAimed => _shotCooldownAimed;
    [SerializeField]
    private FiringMode _firingModes;
    public FiringMode FiringModes => _firingModes;
    [SerializeField]
    private int _burstCount;
    public int BurstCount => _burstCount;

    [Header("Ballistics")]
    [SerializeField]
    private float _projectileMaxRange;
    public float ProjectileMaxRange => _projectileMaxRange;
    [SerializeField]
    private float _projectileSpreadX;
    public float ProjectileSpreadX => _projectileSpreadX;
    [SerializeField]
    private float _projectileSpreadY;
    public float ProjectileSpreadY => _projectileSpreadY;
    [SerializeField]
    private float _accuracySpreadMultiplier;
    public float AccuracySpreadMultiplier => _accuracySpreadMultiplier;
    [Space]
    [SerializeField]
    private float _recoilSnappiness;
    public float RecoilSnappiness => _recoilSnappiness;
    [SerializeField]
    private float _recoilReturnSpeed;
    public float RecoilReturnSpeed => _recoilReturnSpeed;
    [SerializeField]
    private float _recoilRotX;
    public float RecoilRotX => _recoilRotX;
    [SerializeField]
    private float _recoilRotY;
    public float RecoilRotY => _recoilRotY;
    [SerializeField]
    private float _recoilRotZ;
    public float RecoilRotZ => _recoilRotZ;
    [Space]
    [SerializeField]
    private float _recoilPosX;
    public float RecoilPosX => _recoilPosX;
    [SerializeField]
    private float _recoilPosY;
    public float RecoilPosY => _recoilPosY;
    [SerializeField]
    private float _recoilPosZ;
    public float RecoilPosZ => _recoilPosZ;

    [Header("Ammo")]
    [SerializeField]
    private int _maxAmmoInWeapon;
    public int MaxAmmoInWeapon => _maxAmmoInWeapon;
    [SerializeField]
    private List<BaseAmmoDataSheet> _compatibleAmmo;
    public List<BaseAmmoDataSheet> CompatibleAmmo => _compatibleAmmo;

    [Header("Reloading")]
    [SerializeField]
    [Tooltip("Reload time when weapon is empty.")]
    private float _emergencyReloadTime;
    public float EmergencyReloadTime => _emergencyReloadTime;
    [SerializeField]
    [Tooltip("Reload time when weapon is still loaded.")]
    private float _tacticalReloadTime;
    public float TacticalReloadTime => _tacticalReloadTime;

    [Header("SFX")]
    public AudioClip ShootSound;
    public AudioClip EmergencyReloadSound;
}

[Flags]
public enum FiringMode
{
    None = 0,
    Single = 1 << 0,
    Burst = 1 << 1,
    Automatic = 1 << 2,
}
