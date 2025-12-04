public class DamageData
{
    public static float HealthDamage(float damage, float armorPenetration)
    {
        return damage * armorPenetration;
    }

    public static float ArmorDamage(float damage)
    {
        return damage;
    }
}

public enum DamageType
{
    Ballistic,
    Fire,
    Explosive,
    // Electric...? -Shad //
}