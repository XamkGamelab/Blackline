using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageble, IFlammable
{

    public bool IsBurning { get; }

    public void Ignite(float duration, float damagePerSecond)
    {

    }

    public void Extinguish()
    {

    }

    public void ApplyDamage(DamageInfo damageInfo)
    {

    }
}
