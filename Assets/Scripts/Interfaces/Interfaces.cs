public interface IDamageble
{
    SurfaceMaterial SurfaceMaterial { get; }
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

public interface IAmmoProvider
{
    void ConsumeAmmo(BaseAmmoDataSheet ammo, int amount);
    int GetAmmoCount(BaseAmmoDataSheet ammo);
}

public interface IPickup
{
    void Pickup();
}
