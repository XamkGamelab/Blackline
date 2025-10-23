public class DamageData
{
    public static float HealthDamage(float damage, float armorPenetration, float currentArmor)
    {
        if (currentArmor <= 0f) return damage;
        else
        {
            return damage * (armorPenetration * GlobalSettingsHolder.Instance.CurrentDifficultyData.PlayerArmorDamageMitigation);
        }
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