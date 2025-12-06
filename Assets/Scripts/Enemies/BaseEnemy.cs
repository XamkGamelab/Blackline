using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageble
{
    [Header("Base Enemy")]
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _maxArmor;
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    public float _currentHealth { get; protected set; }
    public float _currentArmor { get; protected set; }

    public EnemyStateMachine<BaseEnemy> StateMachine { get; private set; }

    public EnemyState<BaseEnemy> IdleState { get; protected set; }

    private void Awake() => Initialize();

    public virtual void Initialize()
    {
        StateMachine = new EnemyStateMachine<BaseEnemy>();
        IdleState = new BaseEnemyIdleState(this);

        _currentHealth = _maxHealth;
        _currentArmor = _maxArmor;
    }

    private void Update()
    {
        HandleStates();
    }

    public virtual void HandleStates()
    {
        StateMachine.CurrentState.HandleUpdate();
    }

    public void ApplyDamage(float damage, float armorPenetration)
    {
        print($"{gameObject.name} says ouch!");

        _currentHealth -= DamageData.HealthDamage(damage, armorPenetration);
        _currentArmor -= DamageData.ArmorDamage(damage);

        HandleDamage();
    }

    public void HandleDamage()
    {
        if (_currentHealth <= 0f)
        {
            Afterlife();
        }
    }

    public void Afterlife()
    {
        print($"{gameObject.name} is dead!");
    }
}
