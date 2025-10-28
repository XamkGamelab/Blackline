using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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

    public UnityEvent WeaponZoom;

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
        if (!Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey)) return;

        // Logic behind the actual shooting. -Shad //
        for(int i = 0; i < _currentAmmoType.ProjectilesPerShot; i++)
        {
            // Prep the bullet tracer effect. -Shad //
            BulletTracerFX bulletTracerFX = _bulletTracerFXPool.Spawn(_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            // Prep the actual raycast, with its spread. -Shad //
            float spreadX = UnityEngine.Random.Range(-_dataSheet.ProjectileSpreadX, _dataSheet.ProjectileSpreadX);
            float spreadY = UnityEngine.Random.Range(-_dataSheet.ProjectileSpreadY, _dataSheet.ProjectileSpreadY);
            Vector3 spread = new(spreadX, spreadY, 0f);
            Ray bulletRay = new(_bulletSpawnPoint.position, _bulletSpawnPoint.forward + spread);
            RaycastHit bulletHit = new RaycastHit();

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
            if (Physics.Raycast(bulletRay, out bulletHit, _dataSheet.ProjectileMaxRange, _raycastLayers))
            {
                //_currentAmmoType.OnHit(bulletHit, gameObject);

                bulletTracerFX.Engage(bulletHit.point, _currentAmmoType); // Bullet tracer FX! -Shad //
                SurfaceImpactLibrary.Instance.SpawnImpactFX(bulletHit); // Impact FX! -Shad //
            }
            else
            {

                Vector3 dummyPos = _bulletSpawnPoint.position + (_bulletSpawnPoint.forward + spread) * _dataSheet.ProjectileMaxRange; // Dummy position. -Shad //
                bulletTracerFX.Engage(dummyPos, _currentAmmoType); // Only bullet tracer FX! -Shad //
            }
        }
    }

    public override void SecondaryFunction() 
    {
        WeaponZoom?.Invoke();

        // if (!Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.AimKey)) return;
    }

    public override void ThirdFunction()
    {
        if (!Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.WeaponAction)) return;

        CycleFiringMode();

        print(_currentFiringMode);
    }

    public void RefillAmmo()
    {
        _currentAmmo = _dataSheet.MaxAmmoInWeapon;
    }

    // This is all a bunch of ChatGPT black magic. //
    // Even Neo couldn't understand this shit. -Shad //
    private void CycleFiringMode()
    {
        // Quick safety: nothing to cycle through. -ChatGPT //
        if (_dataSheet.FiringModes == 0)
            return;

        // Gather all flag values that are single-bit (1,2,4,8...) and also present in availableFireModes. -ChatGPT //
        var validModes = Enum.GetValues(typeof(FiringMode))
                             .Cast<FiringMode>()
                             .Where(m => m != (FiringMode)0 && ((int)m & ((int)m - 1)) == 0) // Single-bit check. -ChatGPT //
                             .Where(m => (_dataSheet.FiringModes & m) != 0)
                             .OrderBy(m => (int)m)
                             .ToArray();

        if (validModes.Length == 0)
            return; // Nothing available. -ChatGPT //

        // Find index of current mode in the valid list; if not found, start from -1. -ChatGPT //
        int currentIndex = Array.IndexOf(validModes, _currentFiringMode);
        int nextIndex = (currentIndex + 1) % validModes.Length;

        _currentFiringMode = validModes[nextIndex];
    }
}
