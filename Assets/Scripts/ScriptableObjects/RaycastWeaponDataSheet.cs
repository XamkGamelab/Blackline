using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon Data Sheet", menuName = "Data Sheets/Weapons/Create Raycast Weapon Data Sheet")]
public class RaycastWeaponDataSheet : BaseWeaponDataSheet
{
    [Header("Weapon Features")]
    [SerializeField]
    private float _roundsPerSecondFromHip;
    [SerializeField]
    private float _roundsPerSecondAimed;
    [SerializeField]
    private int _maxAmmo;
    [SerializeField]
    private int _projectilesPerShot;
    [SerializeField]
    private float _projectileMaxRange;
    [SerializeField]
    private float _projectileSpreadX;
    [SerializeField]
    private float _projectileSpreadY;

    [Header("Ballistics")]
    [SerializeField]
    private List<BaseAmmoDataSheet> _ammoTypes;

    // This effectively sets the values as read only, but available from other classes. -Shad //
    public float RoundsPerSecondFromHip => _roundsPerSecondFromHip;
    public float RoundsPerSecondAimed => _roundsPerSecondAimed;
    public int MaxAmmo => _maxAmmo;
    public int ProjectilesPerShot => _projectilesPerShot;
    public float ProjectileMaxRange => _projectileMaxRange;
    public float ProjectileSpreadX => _projectileSpreadX;
    public float ProjectileSpreadY => _projectileSpreadY;

    public List<BaseAmmoDataSheet> AmmoTypes => _ammoTypes;
}
