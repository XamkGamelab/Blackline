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
    private Animator AndroidAnimator;

    [Header("Combat Android Weapon")]
    [SerializeField]
    private LayerMask WeaponRaycastLayers;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private BaseAmmoDataSheet AmmoType;
    [SerializeField]
    private WeaponAudio WeaponAudio;

    public RaycastWeaponDataSheet WeaponData => (RaycastWeaponDataSheet)DataSheet.EquippedWeapon;
    public CombatAndroidDataSheet DataSheet => (CombatAndroidDataSheet)BaseEnemyDataSheet;

    public CombatAndroidIdleState IdleState;
    public CombatAndroidPatrolState PatrolState;
    public CombatAndroidChaseState ChaseState;
    public CombatAndroidAttackState AttackState;
    public CombatAndroidDeadState DeadState;
    
    private BulletTracerFXPool _bulletTracerFXPool;
    public float NextShotTime { get; private set; }

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

        PatrolState = new CombatAndroidPatrolState(this);
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
        for (int i = 0; i < AmmoType.ProjectilesPerShot; i++)
        {
            // Prep the bullet tracer effect. -Shad //
            BulletTracerFX bulletTracerFX = _bulletTracerFXPool.Spawn(BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            // Prep the actual raycast, with its spread. -Shad //
            float spreadX = Random.Range(-WeaponData.ProjectileSpreadX, WeaponData.ProjectileSpreadX);
            float spreadY = Random.Range(-WeaponData.ProjectileSpreadY, WeaponData.ProjectileSpreadY);
            Vector3 spread = new(spreadX, spreadY, 0f);
            Ray bulletRay = new(BulletSpawnPoint.position, BulletSpawnPoint.forward + spread);
            RaycastHit bulletHit = new RaycastHit();

            // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
            if (Physics.Raycast(bulletRay, out bulletHit, WeaponData.ProjectileMaxRange, WeaponRaycastLayers))
            {
                bulletTracerFX.Engage(bulletHit.point, AmmoType); // Bullet tracer FX! -Shad //
                SurfaceImpactLibrary.Instance.SpawnImpactFX(bulletHit); // Impact FX! -Shad //

                // The hit object has an IDamageble interface, so let's actually damage that entity. -Shad //
                if (bulletHit.collider.TryGetComponent<IDamageble>(out IDamageble damageble))
                {
                    damageble.ApplyDamage(AmmoType.Damage, AmmoType.ArmorPenetration);
                }
            }
            else
            {
                Vector3 dummyPos = BulletSpawnPoint.position + (BulletSpawnPoint.forward + spread) * WeaponData.ProjectileMaxRange; // Dummy position. -Shad //
                bulletTracerFX.Engage(dummyPos, AmmoType); // Only bullet tracer FX! -Shad //
            }
        }

        // Sound effects. -Shad //
        WeaponAudio.PlayOnce(WeaponData.ShootSound);

        AndroidAnimator.SetTrigger("Shoot");
    }

    private void AndroidAnimation()
    {
        AndroidAnimator.SetFloat("Movement", _agent.velocity.magnitude);
        AndroidAnimator.SetBool("Attack", StateMachine.CurrentState == AttackState);
        AndroidAnimator.SetBool("Dead", StateMachine.CurrentState == DeadState);
    }
}
