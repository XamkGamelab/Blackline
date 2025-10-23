public interface IDamageble
{
    void ApplyDamage(float damage, float armorPenetration);
}

public interface IFlammable
{
    void Ignite(float duration, float damagePerSecond);
    void Extinguish();
    bool IsBurning { get; }
}

public interface IExplosive
{
    void Explode();
}
