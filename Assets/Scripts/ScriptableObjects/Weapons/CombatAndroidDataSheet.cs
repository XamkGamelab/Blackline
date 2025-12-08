using UnityEngine;

[CreateAssetMenu(fileName = "Combat Android Data Sheet", menuName = "Data Sheets/Enemies/Create Combat Android Weapon Data Sheet")]
public class CombatAndroidDataSheet : BaseEnemyDataSheet
{
    [Header("Combat Android Features")]
    [SerializeField]
    private float _engageDistance;
    public float EngageDistance => _engageDistance;
    [SerializeField]
    private float _attackDistance;
    public float AttackDistance => _attackDistance;
    [SerializeField]
    private float _attackFrequency;
    public float AttackFrequency => _attackFrequency;
    [SerializeField]
    private float _patrolPositioningFrequency;
    public float PatrolPositioningFrequency => _patrolPositioningFrequency;
    [SerializeField]
    private float _patrolDistance;
    public float PatrolDistance => _patrolDistance;

    [Space]

    [Header("Combat Andorid Speed")]
    [SerializeField]
    private float _patrolSpeed;
    public float PatrolSpeed => _patrolSpeed;
    [SerializeField]
    private float _walkSpeed;
    public float WalkSpeed => _walkSpeed;

    [Header("SFX")]
    public AudioClip ShootSound;    
}
