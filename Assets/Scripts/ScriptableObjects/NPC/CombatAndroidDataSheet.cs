using UnityEngine;

[CreateAssetMenu(fileName = "Combat Android Data Sheet", menuName = "Data Sheets/Enemies/Create Combat Android Weapon Data Sheet")]
public class CombatAndroidDataSheet : BaseEnemyDataSheet
{
    [Header("Combat Android Features")]
    [SerializeField]
    private float _engageDistance;
    public float EngageDistance => _engageDistance;
    [SerializeField]
    private float _attackDelay;
    public float AttackDelay => _attackDelay;
    [SerializeField]
    private float _attackEndDelay;
    public float AttackEndDelay => _attackEndDelay;
    [SerializeField]
    private float _attackDistance;
    public float AttackDistance => _attackDistance;
    [SerializeField]
    private int _attackMaxAmount;
    public int AttackMaxAmount => _attackMaxAmount;
    [SerializeField]
    private float _repositionDistance;
    public float RepositionDistance => _repositionDistance;
    [SerializeField]
    private float _repositionCooldown;
    public float RepositionCooldown => _repositionCooldown;

    [Space]

    [Header("Combat Andorid Speed")]
    [SerializeField]
    private float _patrolSpeed;
    public float PatrolSpeed => _patrolSpeed;
    [SerializeField]
    private float _walkSpeed;
    public float WalkSpeed => _walkSpeed;

    [Header("SFX")]
    public AudioClip AlertSound;    
}
