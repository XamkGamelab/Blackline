using UnityEngine;
using UnityEngine.AI;

public class CombatAndroid : MonoBehaviour, IDamageble
{
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _maxArmor;

    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    private float _currentHealth;
    private float _currentArmor;

    private void Awake() => Initialize();

    private void Initialize()
    {
        _currentHealth = _maxHealth;
        _currentArmor = _maxArmor;
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
        if(_currentHealth <= 0f)
        {
            Afterlife();
        }
    }

    public void Afterlife()
    {
        print($"{gameObject.name} is dead!");
    }
}
