using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class CombatAndroid : BaseEnemy
{
    [Header("Combat Android")]
    [SerializeField]
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    [SerializeField]
    private Animator _androidAnimator;

    [Header("Combat Android Weapon")]
    [SerializeField]
    private GameObject _weaponObject;
    [SerializeField]
    private LayerMask _weaponRaycastLayers;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private BaseAmmoDataSheet _ammoType;
    [SerializeField]
    private WeaponAudio _weaponAudio;
    [SerializeField]
    private ParticleSystem[] _shootParticles;

    public RaycastWeaponDataSheet WeaponData => (RaycastWeaponDataSheet)DataSheet.EquippedWeapon;
    public CombatAndroidDataSheet DataSheet => (CombatAndroidDataSheet)BaseEnemyDataSheet;

    public CombatAndroidIdleState IdleState;
    public CombatAndroidRepositionState RepositionState;
    public CombatAndroidChaseState ChaseState;
    public CombatAndroidAttackState AttackState;
    public CombatAndroidDeadState DeadState;
    
    private BulletTracerFXPool _bulletTracerFXPool;
    public float NextShotTime { get; private set; }
    [HideInInspector] public float NextRepositionTime;

    [Inject]
    private void Construct(BulletTracerFXPool pool)
    {
        _bulletTracerFXPool = pool;
    }

    private void Awake() => Initialize();

    public override void Initialize()
    {
        base.Initialize();

        NextShotTime = Time.time;
        NextRepositionTime = Time.time;

        RepositionState = new CombatAndroidRepositionState(this);
        IdleState = new CombatAndroidIdleState(this);
        ChaseState = new CombatAndroidChaseState(this);
        AttackState = new CombatAndroidAttackState(this);
        DeadState = new CombatAndroidDeadState(this);

        StateMachine.UpdateState(IdleState);
    }

    public override void Update()
    {
        base.Update();

        AndroidAnimation();
    }    

    public void ShootRevolver()
    {
        //if (CurrentFiringMode == FiringMode.Burst) BurstShotsRemaining--;

        //LoadedAmmoCount--;

        NextShotTime = Time.time + WeaponData.ShotCooldownFromHip;

        // Logic behind the actual shooting. -Shad //
        for (int i = 0; i < _ammoType.ProjectilesPerShot; i++)
        {
            // Prep the bullet tracer effect. -Shad //
            BulletTracerFX bulletTracerFX = _bulletTracerFXPool.Spawn(_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            // Prep the actual raycast, with its spread. -Shad //
            float spreadX = Random.Range(-WeaponData.ProjectileSpreadX, WeaponData.ProjectileSpreadX);
            float spreadY = Random.Range(-WeaponData.ProjectileSpreadY, WeaponData.ProjectileSpreadY);
            Vector3 spread = new(spreadX, spreadY, 0f);
            Ray bulletRay = new(_bulletSpawnPoint.position, _bulletSpawnPoint.forward + spread);
            RaycastHit bulletHit = new RaycastHit();

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
            if (Physics.Raycast(bulletRay, out bulletHit, WeaponData.ProjectileMaxRange, _weaponRaycastLayers))
            {
                bulletTracerFX.Engage(bulletHit.point, _ammoType); // Bullet tracer FX! -Shad //
                SurfaceImpactLibrary.Instance.SpawnImpactFX(bulletHit); // Impact FX! -Shad //

                // The hit object has an IDamageble interface, so let's actually damage that entity. -Shad //
                if (bulletHit.collider.TryGetComponent<IDamageble>(out IDamageble damageble))
                {
                    damageble.ApplyDamage(_ammoType.Damage, _ammoType.ArmorPenetration);
                }
            }
            else
            {
                Vector3 dummyPos = _bulletSpawnPoint.position + (_bulletSpawnPoint.forward + spread) * WeaponData.ProjectileMaxRange; // Dummy position. -Shad //
                bulletTracerFX.Engage(dummyPos, _ammoType); // Only bullet tracer FX! -Shad //
            }
        }

        foreach (ParticleSystem fx in _shootParticles) fx.Play();

        // Sound effects. -Shad //
        _weaponAudio.PlayOnce(WeaponData.ShootSound);

        _androidAnimator.SetTrigger("Shoot");
    }

    private void AndroidAnimation()
    {
        _androidAnimator.SetFloat("Movement", _agent.velocity.magnitude);
        _androidAnimator.SetBool("Attack", StateMachine.CurrentState == AttackState && StateMachine.CurrentState != RepositionState);
        _androidAnimator.SetBool("Dead", StateMachine.CurrentState == DeadState);
    }
}
