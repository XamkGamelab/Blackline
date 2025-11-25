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
    public RaycastWeaponDataSheet DataSheet => _dataSheet;

    public bool _dualWielding { get; private set; }

    private BaseAmmoDataSheet _currentAmmoType;

    [HideInInspector] public UnityEvent WeaponZoomEvent;

    public FiringMode CurrentFiringMode => _currentFiringMode;

    public RaycastWeaponFiringState FiringState;
    public RaycastWeaponReloadState ReloadState;

    public int LoadedAmmoCount;
    public int BurstShotsRemaining { get; private set; }

    public float NextShotTime { get; private set; }

    [Inject]
    public void Construct(BulletTracerFXPool bulletTracerFXPool)
    {
        _bulletTracerFXPool = bulletTracerFXPool;
    }

    public override void Initialize()
    {
        base.Initialize();

        IdleState = new RaycastWeaponIdleState(this);
        FiringState = new RaycastWeaponFiringState(this);
        ReloadState = new RaycastWeaponReloadState(this);
        NextShotTime = Time.time;

        StateMachine.UpdateState(DrawState);

        _currentAmmoType = _dataSheet.CompatibleAmmo[0];
        LoadedAmmoCount = _dataSheet.MaxAmmoInWeapon;
    }

    #region Main Functions
    public override void PrimaryFunction()
    {
        if (CurrentFiringMode == FiringMode.Burst) BurstShotsRemaining--;

        LoadedAmmoCount--;

        NextShotTime = Time.time + _dataSheet.ShotCooldownFromHip;

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

        // Sound effects. -Shad //
        WeaponAudio.PlayOnce(DataSheet.ShootSound);
    }

    public override void SecondaryFunction() 
    {
        WeaponZoomEvent?.Invoke();

        // if (!Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.AimKey)) return;
    }

    public override void ThirdFunction()
    {
        if (StateMachine.CurrentState != IdleState) return;
        if (!Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.UniqueWeaponAction)) return;

        CycleFiringMode();

        print(_currentFiringMode);
    }

    // This is all a bunch of ChatGPT black magic. //
    // Even Neo Anderson couldn't understand this shit. -Shad //
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

    public void ReloadWeapon()
    {
        LoadedAmmoCount = _dataSheet.MaxAmmoInWeapon;
    }
    #endregion
   
    public void CalculateBurstCount()
    {
        if (LoadedAmmoCount < _dataSheet.BurstCount) BurstShotsRemaining = LoadedAmmoCount;
        else
        {
            BurstShotsRemaining = _dataSheet.BurstCount;
        }

        BurstShotsRemaining = _dataSheet.BurstCount;
    }

    public bool AmmoLeftInWeapon() { return LoadedAmmoCount > 0; }

    public override void HandleFunctions()
    {
        base.HandleFunctions();

        //ThirdFunction();
    }
}
