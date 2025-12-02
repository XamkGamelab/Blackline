using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageble, IFlammable
{
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;
      
    public float CurrentHealth { get; private set; }
    public float CurrentArmor { get; private set; }

    public event Action DamageTakenEvent;

    private PlayerDataSheet PlayerData;    

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        PlayerData = playerDataSheet;
    }

    private void Awake()
    {
        CurrentHealth = 0;
        CurrentArmor = 0;
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
        CurrentHealth -= DamageData.HealthDamage(damage, armorPenetration, CurrentArmor);

        DamageTakenEvent?.Invoke();
    }
}
