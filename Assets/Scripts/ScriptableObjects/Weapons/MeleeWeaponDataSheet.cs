using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon Data Sheet", menuName = "Data Sheets/Weapons/Create Melee Weapon Data Sheet")]
public class MeleeWeaponDataSheet : BaseWeaponDataSheet
{
    [Header("Weapon Features")]
    [SerializeField]
    private float _lightAttackCooldown;
    public float LightAttackCooldown => _lightAttackCooldown;
    [SerializeField]
    private float _heavyAttackCooldown;
    public float HeavyAttackCooldown => _heavyAttackCooldown;
    [SerializeField]
    private float _attackInputBufferTime;
    public float AttackInputBufferTime => _attackInputBufferTime;

    [Header("Ballistics")]
    [SerializeField]
    private float _swingMaxRange;
    public float SwingMaxRange => _swingMaxRange;

    [Header("Recoil")]
    [SerializeField]
    private float _recoilRotX;
    public float RecoilRotX => _recoilRotX;
    [SerializeField]
    private float _recoilRotY;
    public float RecoilRotY => _recoilRotY;
    [SerializeField]
    private float _recoilRotZ;
    public float RecoilRotZ => _recoilRotZ;

    [Header("SFX")]
    public AudioClip[] SwingSounds;    
}
