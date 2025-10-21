using System;
using UnityEditor.ShaderGraph.Internal;
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
    private float _movementSmoothingRateGrounded;
    [SerializeField]
    private float _movementSmoothingRateAirborne;

    [Header("Sliding")]
    [SerializeField]
    private float _slideSpeedMultiplier;
    [SerializeField]
    private float _slideSmoothingRate;
    [SerializeField]
    private float _slideCooldown;

    [Header("Bunnyhopping")]
    [SerializeField]
    private float _bunnyHopSpeedMultiplier;
    [SerializeField]
    private float _bunnyHopSmoothingRateGrounded;
    [SerializeField]
    private float _bunnyHopSmoothingRateAirborne;

    [Header("Perks")]
    [SerializeField]
    private Perk[] _perks;

    public float WalkingCameraFOV => _walkingCameraFOV;
    public float RunningCameraFOV => _runningCameraFOV;

    public float CharControlDefaultCenterY => _charControlDefaultCenterY;
    public float CharControlDefaultHeight => _charControlDefaultHeight;
    public float CharControlCrouchCenterY => _charControlCrouchCenterY;
    public float CharControlCrouchHeight => _charControlCrouchHeight;

    public float MovementSmoothingRateGrounded => _movementSmoothingRateGrounded;
    public float MovementSmoothingRateAirborne => _movementSmoothingRateAirborne;

    public float SlideSpeedMultiplier => _slideSpeedMultiplier;
    public float SlideSmoothingRate => _slideSmoothingRate;
    public float SlideCooldown => _slideCooldown;    

    public float BunnyHopSpeedMultiplier => _bunnyHopSpeedMultiplier;
    public float BunnyHopSmoothingRateGrounded => _bunnyHopSmoothingRateGrounded;
    public float BunnyHopSmoothingRateAirborne => _bunnyHopSmoothingRateAirborne;

    public Perk[] Perks => _perks;
}

[Serializable]
public class Perk
{
    [SerializeField]
    private string _perkName;
}
