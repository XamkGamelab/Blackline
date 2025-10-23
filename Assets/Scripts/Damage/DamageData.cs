public class DamageData
{
    public static float HealthDamage(float damage, float armorPenetration, float maxArmor, float currentArmor)
    {
        return 0f;
    }

    public static float ArmorDamage(float damage, float armorPenetration)
    {
        return damage * armorPenetration;
    }
}

public enum DamageType
{
    Ballistic,
    Fire,
    Explosive,
    // Electric...? -Shad //
    // Magic...? -Shad //
    // Demonic...? -Shad //
}