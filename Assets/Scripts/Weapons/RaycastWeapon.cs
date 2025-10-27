using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class RaycastWeapon : BaseWeapon
{
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private LayerMask _raycastLayers;
    [SerializeField]
    private FiringMode _currentFiringMode;

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

    public override void ThirdFunction()
    {
        CycleFiringMode();

        print(_currentFiringMode);
    }

    public void RefillAmmo()
    {
        _currentAmmo = _dataSheet.MaxAmmoInWeapon;
    }

    // This is all a bunch of ChatGPT black magic. -Shad //
    private void CycleFiringMode()
    {
        // quick safety: nothing to cycle through
        if (_dataSheet.FiringModes == 0)
            return;

        // Gather all flag values that are single-bit (1,2,4,8...) and also present in availableFireModes
        var validModes = Enum.GetValues(typeof(FiringMode))
                             .Cast<FiringMode>()
                             .Where(m => m != (FiringMode)0 && ((int)m & ((int)m - 1)) == 0) // single-bit check
                             .Where(m => (_dataSheet.FiringModes & m) != 0)
                             .OrderBy(m => (int)m)
                             .ToArray();

        if (validModes.Length == 0)
            return; // nothing available

        // Find index of current mode in the valid list; if not found, start from -1
        int currentIndex = Array.IndexOf(validModes, _currentFiringMode);
        int nextIndex = (currentIndex + 1) % validModes.Length;

        _currentFiringMode = validModes[nextIndex];
    }
}
