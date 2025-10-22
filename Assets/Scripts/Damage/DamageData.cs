using UnityEngine;

public enum DamageType
{
    Ballistic,
    Fire,
    Explosive,
    // Electric...? -Shad //
    // Magic...? -Shad //
    // Demonic...? -Shad //
}

[System.Serializable]
public struct DamageInfo
{
    public DamageType DamageType; // The damage type, as seen above. -Shad //
    public float DamageAmount; // The damage amount, dumbass. -Shad //
    public float ArmorPenetration; // Armor pen- are you fucking blind? -Shad //
    public Vector3 HitPoint; // The position of the hit raycast. -Shad //
    public Vector3 HitNormal; // The normal of the hit raycast. -Shad //
    // public ??? Source - We need a way to determine where the damage comes from. -Shad //

    public DamageInfo(DamageType damageType, float damageAmount, float armorPenetration, Vector3 hitPoint, Vector3 hitNormal)
    {
        DamageType = damageType;
        DamageAmount = damageAmount;
        ArmorPenetration = armorPenetration;
        HitPoint = hitPoint;
        HitNormal = hitNormal;
    }
}