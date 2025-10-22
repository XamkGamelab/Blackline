public interface IDamageble
{
    void ApplyDamage(DamageInfo damageInfo);
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
