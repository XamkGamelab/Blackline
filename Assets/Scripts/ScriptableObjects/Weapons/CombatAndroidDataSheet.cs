using UnityEngine;

[CreateAssetMenu(fileName = "Combat Android Data Sheet", menuName = "Data Sheets/Enemies/Create Combat Android Weapon Data Sheet")]
public class CombatAndroidDataSheet : BaseEnemyDataSheet
{
    [Header("Combat Android Features")]
    [SerializeField]
    private float _lightAttackCooldown;
    public float LightAttackCooldown => _lightAttackCooldown;
    [SerializeField]
    private float _heavyAttackCooldown;
    public float HeavyAttackCooldown => _heavyAttackCooldown;
    [SerializeField]
    private float _attackInputBufferTime;
    public float AttackInputBufferTime => _attackInputBufferTime;

    [Header("Combat Android Damage")]
    [SerializeField]
    private float _attackDamage;
    public float AttackDamage => _attackDamage;
    [SerializeField]
    [Range(0f, 1f)]
    private float _armorPenetration;
    public float ArmorPenetration => _armorPenetration;

    [Header("SFX")]
    public AudioClip ShootSound;    
}
