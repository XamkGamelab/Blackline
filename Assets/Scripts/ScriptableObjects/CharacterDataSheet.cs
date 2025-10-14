using UnityEngine;

public abstract class CharacterDataSheet : ScriptableObject
{
    [Header("Numerical Values")]
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _crouchSpeed;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _maxArmor;

    [Header("Inventory")]
    [SerializeField]
    private int _maxMeleeWeapons;
    [SerializeField]
    private int _maxUtilityWeapons;
    [SerializeField]
    private int _maxMedkits;
    [SerializeField]
    private int _maxPowerups;

    // Doing this is a lot more code, but more future proof with more control. -Davoth //
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float CrouchSpeed => _crouchSpeed;
    public float Gravity => _gravity;
    public float JumpHeight => _jumpHeight;

    public int MaxMeleeWeapons => _maxMeleeWeapons;
    public int MaxUtilityWeapons => _maxUtilityWeapons;
    public int MaxMedkits => _maxMedkits;
    public int MaxPowerups => _maxPowerups;
}
