using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data Sheet", menuName = "Data Sheets/Create Player Data Sheet")]
public class PlayerDataSheet : CharacterDataSheet
{
    [Header("Cinematics")]
    [SerializeField]
    [Range(60f, 180f)]
    private float _walkingCameraFOV;
    [SerializeField]
    [Range(60f, 180f)]
    private float _runningCameraFOV;

    [Header("Player Character Controller")]
    [SerializeField]
    private float _charControlDefaultCenterY;
    [SerializeField]
    private float _charControlDefaultHeight;
    [SerializeField]
    private float _charControlCrouchCenterY;
    [SerializeField]
    private float _charControlCrouchHeight;

    [Header("Player Movement")]
    [SerializeField]
    private float _movementSmoothingTime;
    [SerializeField]
    private float _slideCooldown;
    [SerializeField]
    private float _slideSpeedMultiplier;
    [SerializeField]
    private float _runJumpSpeedMultiplier;

    [Header("Perks")]
    [SerializeField]
    private Perk[] _perks;

    public float WalkingCameraFOV => _walkingCameraFOV;
    public float RunningCameraFOV => _runningCameraFOV;

    public float CharControlDefaultCenterY => _charControlDefaultCenterY;
    public float CharControlDefaultHeight => _charControlDefaultHeight;
    public float CharControlCrouchCenterY => _charControlCrouchCenterY;
    public float CharControlCrouchHeight => _charControlCrouchHeight;

    public float MovementSmoothingTime => _movementSmoothingTime;
    public float SlideCooldown => _slideCooldown;
    public float SlideSpeedMultiplier => _slideSpeedMultiplier;
    public float RunJumpSpeedMultiplier => _runJumpSpeedMultiplier;

    public Perk[] Perks => _perks;
}

[Serializable]
public class Perk
{
    [SerializeField]
    private string _perkName;
}
