using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageble, IFlammable, IPlayerHealth
{
    [SerializeField]
    private PlayerMovement _playerMovement; // Fucking dependancy hack. Fuck this fuck you. -Shad //
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;
      
    public float CurrentHealth { get; private set; }
    public float CurrentArmor { get; private set; }

    public event Action DamageTakenEvent;

    private PlayerDataSheet PlayerData;

    public float IPlayerHealth => CurrentHealth;
    public float IPlayerArmor => CurrentArmor;

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        PlayerData = playerDataSheet;
    }

    private void Awake()
    {
        CurrentHealth = PlayerData.MaxHealth;
        CurrentArmor = PlayerData.MaxArmor;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, PlayerData.MaxHealth);
        CurrentArmor = Mathf.Clamp(CurrentArmor, 0f, PlayerData.MaxArmor);
    }

    public bool IsBurning { get; private set; }
    public void Ignite(float duration, float damagePerSecond)
    {

    }

    public void Extinguish()
    {

    }

    public void ApplyDamage(float damage, float armorPenetration)
    {
        if (CurrentHealth - DamageData.HealthDamage(damage, armorPenetration) <= 0f) CurrentHealth = 0f;
        else CurrentHealth -= DamageData.HealthDamage(damage, armorPenetration);

        if(CurrentArmor - DamageData.ArmorDamage(damage) <= 0f) CurrentArmor = 0f;
        else CurrentArmor -= DamageData.ArmorDamage(damage);

        if (CurrentHealth <= 0f) _playerMovement.UpdateState(_playerMovement.DeadState);

        DamageTakenEvent?.Invoke();
    }

    public bool CanAddArmor()
    {
        return CurrentArmor < PlayerData.MaxArmor;
    }

    public void AddArmor(float added)
    {
        CurrentArmor += added;
    }

    public bool CanAddHealth()
    {
        return CurrentHealth < PlayerData.MaxHealth;
    }

    public void AddHealth(float added)
    {
        CurrentHealth += added;
    }
}
