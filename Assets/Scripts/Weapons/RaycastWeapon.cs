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
        //_currentAmmoType = (BallisticAmmoDataSheet)_currentAmmoType;
    }

    public override void PrimaryFunction()
    {
        for(int i = 0; i < _dataSheet.ProjectilesPerShot; i++)
        {
            Ray bulletRay = new(_bulletSpawnPoint.position, _bulletSpawnPoint.forward);
            RaycastHit bulletHit = new RaycastHit();

            BulletTracerFX bulletTracerFX = _bulletTracerFXPool.Spawn(_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
            if (Physics.Raycast(bulletRay, out bulletHit, _dataSheet.ProjectileMaxRange, _raycastLayers))
            {
                _currentAmmoType.OnHit(bulletHit, gameObject);

                bulletTracerFX.Engage(bulletHit.point, _currentAmmoType); // Only FX! -Shad //
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
