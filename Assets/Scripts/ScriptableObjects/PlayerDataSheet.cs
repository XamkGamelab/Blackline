using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data Sheet", menuName = "Data Sheets/Create Player Data Sheet")]
public class PlayerDataSheet : CharacterDataSheet
{
    [Header("Cinematics")]
    [SerializeField]
    [Range(60f, 180f)]
    private float _walkingCameraFOV;
    public float WalkingCameraFOV => _walkingCameraFOV;
    [SerializeField]
    [Range(60f, 180f)]
    private float _runningCameraFOV;
    public float RunningCameraFOV => _runningCameraFOV;
    [SerializeField]
    private float _cameraDefaultPosY;
    public float CameraDefaultPosY => _cameraDefaultPosY;
    [SerializeField]
    private float _cameraCrouchPosY;
    public float CameraCrouchPosY => _cameraCrouchPosY;
    [SerializeField]
    private float _cameraDeadPosY;
    public float CameraDeadPosY => _cameraDeadPosY;

    [Header("Player Character Controller")]
    [SerializeField]
    private float _charControlDefaultCenterY;
    public float CharControlDefaultCenterY => _charControlDefaultCenterY;
    [SerializeField]
    private float _charControlDefaultHeight;
    public float CharControlDefaultHeight => _charControlDefaultHeight;
    [SerializeField]
    private float _charControlCrouchCenterY;
    public float CharControlCrouchCenterY => _charControlCrouchCenterY;
    [SerializeField]
    private float _charControlCrouchHeight;
    public float CharControlCrouchHeight => _charControlCrouchHeight;
    [SerializeField]
    private float _crouchTransitionSpeed;
    public float CrouchTransitionSpeed => _crouchTransitionSpeed;

    [Header("Player Movement")]
    [SerializeField]
    private float _movementSmoothingTimeGrounded;
    public float MovementSmoothingTimeGrounded => _movementSmoothingTimeGrounded;
    [SerializeField]
    private float _movementSmoothingTimeAirborne;
    public float MovementSmoothingTimeAirborne => _movementSmoothingTimeAirborne;

    [Header("Sliding")]
    [SerializeField]
    private float _slideSpeedMultiplier;
    public float SlideSpeedMultiplier => _slideSpeedMultiplier;
    [SerializeField]
    private float _slideOutSmoothingTime;
    public float SlideOutSmoothingTime => _slideOutSmoothingTime;
    [SerializeField]
    private float _slideInSmoothingTime;
    public float SlideInSmoothingTime => _slideInSmoothingTime;
    [SerializeField]
    private float _slideCooldown;
    public float SlideCooldown => _slideCooldown;

    [Header("Bunnyhopping")]
    [SerializeField]
    private float _bunnyHopSpeedMultiplier;
    public float BunnyHopSpeedMultiplier => _bunnyHopSpeedMultiplier;
    [SerializeField]
    private float _bunnyHopSmoothingTimeGrounded;
    public float BunnyHopSmoothingTimeGrounded => _bunnyHopSmoothingTimeGrounded;
    [SerializeField]
    private float _bunnyHopSmoothingTimeAirborne;
    public float BunnyHopSmoothingTimeAirborne => _bunnyHopSmoothingTimeAirborne;

    [Header("Perks")]
    [SerializeField]
    private Perk[] _perks;
    public Perk[] Perks => _perks;
}

[Serializable]
public class Perk
{
    [SerializeField]
    private string _perkName;
}
