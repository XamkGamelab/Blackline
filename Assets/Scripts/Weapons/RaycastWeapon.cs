using UnityEngine;
using Zenject;

public class RaycastWeapon : BaseWeapon
{
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private LayerMask _raycastLayers;

    private BulletPool _bulletPool;

    private RaycastWeaponDataSheet _dataSheet => (RaycastWeaponDataSheet)WeaponData;

    private bool _isAiming;
    private int _currentAmmo;

    private BaseAmmoDataSheet _currentAmmoType;

    [Inject]
    public void Construct(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Awake()
    {
        //_currentAmmoType = (BallisticAmmoDataSheet)_currentAmmoType;
    }

    public override void PrimaryFunction()
    {
        print("Pew!");

        for(int i = 0; i < _dataSheet.ProjectilesPerShot; i++)
        {
            Ray bulletRay = new(_bulletSpawnPoint.position, _bulletSpawnPoint.forward);
            RaycastHit bulletHit = new RaycastHit();

            BulletTracerFX bullet = _bulletPool.Spawn(_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Davoth //
            if (Physics.Raycast(bulletRay, out bulletHit, _dataSheet.ProjectileMaxRange, _raycastLayers))
            {
                bullet.Fire(bulletHit.point, _currentAmmoType);
            }
            else
            {
                bullet.Fire(_bulletSpawnPoint.forward * _dataSheet.ProjectileMaxRange, _currentAmmoType);
            }
        }
    }

    public override void SecondaryFunction() 
    {
        print("The other pew!");
    }

    public void RefillAmmo()
    {
        _currentAmmo = _dataSheet.MaxAmmo;
    }
}
