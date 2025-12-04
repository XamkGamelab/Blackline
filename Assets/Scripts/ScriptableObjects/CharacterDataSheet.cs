using UnityEngine;

public abstract class CharacterDataSheet : ScriptableObject
{
    [Header("Numerical Values")]
    [SerializeField]
    private float _walkSpeed;
    public float WalkSpeed => _walkSpeed;
    [SerializeField]
    private float _runSpeed;
    public float RunSpeed => _runSpeed;
    [SerializeField]
    private float _crouchSpeed;
    public float CrouchSpeed => _crouchSpeed;
    [SerializeField]
    private float _staticGravity;
    public float StaticGravity => _staticGravity;
    [SerializeField]
    private float _jumpGravity;
    public float JumpGravity => _jumpGravity;
    [SerializeField]
    private float _jumpHeight;
    public float JumpHeight => _jumpHeight;
    [SerializeField]
    private float _maxHealth;
    public float MaxHealth => _maxHealth;
    [SerializeField]
    private float _maxArmor;
    public float MaxArmor => _maxArmor;
}
