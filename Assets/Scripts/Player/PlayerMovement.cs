using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
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

    // This is the final vector to actually move the character controller. //
    // The Character Controlller component doesn't work properly when the //
    // Move method is called more than once per frame. Fuck you Unity <3. -Shad //
    private Vector3 _finalSumVector;

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

        UpdateState(IdleState);
    }

    private void Update()
    {
        CurrentState.HandleInput();
        CurrentState.HandleUpdate();

        _finalSumVector = MoveVector + GravityVector + BunnyhopVector + SlideVector;

        CharacterController.Move(_finalSumVector * Time.deltaTime);
    }

    public void UpdateState(PlayerBaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    public void SetCrouchSize(bool crouching)
    {
        if (crouching)
        {
            CharacterController.center = new(0f, PlayerData.CharControlCrouchCenterY, 0f);
            CharacterController.height = PlayerData.CharControlCrouchHeight;
            _cameraPivot.localPosition = new(0f, PlayerData.CameraCrouchPosY, 0f);
        }
        else
        {
            CharacterController.center = new(0f, PlayerData.CharControlDefaultCenterY, 0f);
            CharacterController.height = PlayerData.CharControlDefaultHeight;
            _cameraPivot.localPosition = new(0f, PlayerData.CameraDefaultPosY, 0f);
        }
    }
}
