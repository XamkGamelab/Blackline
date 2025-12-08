using UnityEngine;
using Zenject;

public abstract class BaseEnemy : MonoBehaviour, IDamageble
{
    [Header("Base Enemy")]
    [SerializeField]
    private BaseEnemyDataSheet _baseEnemyDataSheet;
    public BaseEnemyDataSheet BaseEnemyDataSheet => _baseEnemyDataSheet;
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    public float _currentHealth { get; protected set; }
    public float _currentArmor { get; protected set; }

    public EnemyStateMachine<BaseEnemy> StateMachine { get; private set; }

    public IPlayerPosition PlayerPosition { get; protected set; }
    public IPlayerHealth PlayerHealth { get; protected set; }

    [Inject]
    private void Construct(IPlayerPosition playerPos, IPlayerHealth playerHealth)
    {
        PlayerPosition = playerPos;
        PlayerHealth = playerHealth;
    }

    private void Awake() => Initialize();

    public virtual void Initialize()
    {
        StateMachine = new EnemyStateMachine<BaseEnemy>();

        _currentHealth = BaseEnemyDataSheet.MaxHealth;
        _currentArmor = BaseEnemyDataSheet.MaxArmor;
    }

    public virtual void Update()
    {
        HandleStates();
    }

    private void HandleStates()
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
