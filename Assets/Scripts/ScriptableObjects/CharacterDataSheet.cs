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
    private float _gravity;
    public float Gravity => _gravity;
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
