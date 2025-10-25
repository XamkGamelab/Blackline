using UnityEngine;
using Zenject;

public class RaycastWeapon : BaseWeapon
{
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private LayerMask _raycastLayers;

    private BulletTracerFXPool _bulletTracerFXPool;

    private RaycastWeaponDataSheet _dataSheet => (RaycastWeaponDataSheet)WeaponData;

    private bool _isAiming;
    private int _currentAmmo;

    private BaseAmmoDataSheet _currentAmmoType;

    [Inject]
    public void Construct(BulletTracerFXPool bulletTracerFXPool)
    {
        _bulletTracerFXPool = bulletTracerFXPool;
    }

    public void Awake()
    {
        _currentAmmoType = _dataSheet.CompatibleAmmo[0];
    }

    public override void PrimaryFunction()
    {
        for(int i = 0; i < _dataSheet.ProjectilesPerShot; i++)
        {
            // Prep the bullet tracer effect. -Shad //
            BulletTracerFX bulletTracerFX = _bulletTracerFXPool.Spawn(_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            // Prep the actual raycast, with its spread. -Shad //
            Vector3 spread = new(_dataSheet.ProjectileSpreadX, _dataSheet.ProjectileSpreadY, 0f);
            Ray bulletRay = new(_bulletSpawnPoint.position, _bulletSpawnPoint.forward + spread);
            RaycastHit bulletHit = new RaycastHit();

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
            if (Physics.Raycast(bulletRay, out bulletHit, _dataSheet.ProjectileMaxRange, _raycastLayers))
            {
                //_currentAmmoType.OnHit(bulletHit, gameObject);

                bulletTracerFX.Engage(bulletHit.point, _currentAmmoType); // Only FX! -Shad //
                SurfaceImpactLibrary.Instance.SpawnImpactFX(bulletHit); // Only FX! -Shad //
            }
            else
            {

                bulletTracerFX.Engage(_bulletSpawnPoint.forward * _dataSheet.ProjectileMaxRange, _currentAmmoType); // Only FX! -Shad //
            }
        }
    }

    public override void SecondaryFunction() 
    {
        
    }

    public void RefillAmmo()
    {
        _currentAmmo = _dataSheet.MaxAmmo;
    }
}
