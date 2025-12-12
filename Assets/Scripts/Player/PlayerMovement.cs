using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IPlayerPosition
{
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private Transform _cameraPivot;

    // Zenject dependency injection. //
    [HideInInspector]
    public PlayerDataSheet PlayerData;

    // Player States. //
    public PlayerBaseState CurrentState;
    public PlayerIdleState IdleState;
    public PlayerWalkState WalkState;
    public PlayerRunState RunState;
    public PlayerCrouchState CrouchState;
    public PlayerSlideState SlideState;
    public PlayerJumpState JumpState;
    public PlayerFallingState FallingState;
    public PlayerDeadState DeadState;

    // Public, but hidden references. //
    public CharacterController CharacterController => _characterController;
    [HideInInspector]
    public Vector3 InputVector;
    [HideInInspector]
    public Vector3 MoveVector;
    [HideInInspector]
    public Vector3 GravityVector;
    [HideInInspector]
    public Vector3 BunnyhopVector;
    [HideInInspector]
    public Vector3 SlideVector;
    [HideInInspector]
    public Vector3 TempDirectionVector;
    [HideInInspector]
    public Vector3 RefVector = Vector3.zero;

    [HideInInspector]
    public float CurrentGravitySpeed;

    // This is the final vector to actually move the character controller. //
    // The Character Controlller component doesn't work properly when the //
    // Move method is called more than once per frame. Fuck you Unity <3. -Shad //
    private Vector3 _finalSumVector;

    private bool _crouching;
    private float _targetCenterY, _targetHeight, _targetCamHeight;

    // IPlayerPosition for Enemies. -Shad //
    [HideInInspector] 
    public Transform IPlayerTransform => transform;

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        PlayerData = playerDataSheet;
    }

    private void Awake()
    {
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        CrouchState = new PlayerCrouchState(this);
        SlideState = new PlayerSlideState(this);
        JumpState = new PlayerJumpState(this);
        FallingState = new PlayerFallingState(this);
        DeadState = new PlayerDeadState(this);

        UpdateState(IdleState);
    }

    private void Update()
    {
        CurrentState.HandleInput();
        CurrentState.HandleUpdate();

        _finalSumVector = MoveVector + GravityVector + BunnyhopVector + SlideVector;

        /*print($"MoveVector: {MoveVector.magnitude}");
        print($"GravityVector: {GravityVector.magnitude}");
        print($"BunnyhopVector: {BunnyhopVector.magnitude}");
        print($"SlideVector: {SlideVector.magnitude}");
        print($"State: {CurrentState}");*/

        CharacterController.Move(_finalSumVector * Time.deltaTime);

        if (_crouching && CurrentState != DeadState)
        {
            _targetCenterY = Mathf.Lerp(_targetCenterY, PlayerData.CharControlCrouchCenterY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetHeight = Mathf.Lerp(_targetHeight, PlayerData.CharControlCrouchHeight, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetCamHeight = Mathf.Lerp(_targetCamHeight, PlayerData.CameraCrouchPosY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
        }
        else if(!_crouching && CurrentState != DeadState)
        {
            _targetCenterY = Mathf.Lerp(_targetCenterY, PlayerData.CharControlDefaultCenterY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetHeight = Mathf.Lerp(_targetHeight, PlayerData.CharControlDefaultHeight, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetCamHeight = Mathf.Lerp(_targetCamHeight, PlayerData.CameraDefaultPosY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
        }
        else
        {
            _targetCenterY = Mathf.Lerp(_targetCenterY, PlayerData.CharControlCrouchCenterY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetHeight = Mathf.Lerp(_targetHeight, PlayerData.CharControlCrouchHeight, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
            _targetCamHeight = Mathf.Lerp(_targetCamHeight, PlayerData.CameraDeadPosY, PlayerData.CrouchTransitionSpeed * Time.deltaTime);
        }

        CharacterController.center = new(0f, _targetCenterY, 0f);
        CharacterController.height = _targetHeight;
        _cameraPivot.localPosition = new(0f, _targetCamHeight, 0f);
    }

    public void UpdateState(PlayerBaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    public void SetCrouchSize(bool crouching)
    {
        _crouching = crouching;
    }
}
