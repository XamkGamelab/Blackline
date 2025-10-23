using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageble, IFlammable
{
    private float _currentArmor;
    private float _currentHealth;

    private PlayerDataSheet PlayerData;

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        PlayerData = playerDataSheet;
    }

    public bool IsBurning { get; }
    public void Ignite(float duration, float damagePerSecond)
    {

    }

    public void Extinguish()
    {

    }

    public void ApplyDamage(float damage, float armorPenetration)
    {
        _currentHealth -= damage;
    }
}
