using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon Data Sheet", menuName = "Data Sheets/Weapons/Create Melee Weapon Data Sheet")]
public class MeleeWeaponDataSheet : BaseWeaponDataSheet
{
    [Header("Weapon Features")]
    [SerializeField]
    private float _swingCooldown;
    public float SwingCooldown => _swingCooldown;

    [Header("Ballistics")]
    [SerializeField]
    private float _swingMaxRange;
    public float SwingMaxRange => _swingMaxRange;

    [Header("SFX")]
    public AudioClip[] SwingSounds;    
}
