using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    // Zenject dependency injection. //
    [HideInInspector]
    public PlayerDataSheet PlayerData;

    // Player States. //
    [HideInInspector]
    public PlayerBaseState CurrentState;
    [HideInInspector]
    public PlayerWalkState WalkState;
    [HideInInspector]
    public PlayerRunState RunState;
    [HideInInspector]
    public PlayerCrouchState CrouchState;
    [HideInInspector]
    public PlayerSlideState SlideState;
    [HideInInspector]
    public PlayerJumpState JumpState;
    [HideInInspector]
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

    private Vector3 _finalSumVector;

    [Inject]
    public void Construct(PlayerDataSheet playerDataSheet)
    {
        PlayerData = playerDataSheet;
    }

    private void Awake()
    {
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        CrouchState = new PlayerCrouchState(this);
        SlideState = new PlayerSlideState(this);
        JumpState = new PlayerJumpState(this);
        FallingState = new PlayerFallingState(this);

        UpdateState(WalkState);
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
        if (CurrentState != null) CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }

    public void SetControllerSize(bool crouching)
    {
        if (crouching)
        {
            CharacterController.center = new(0f, PlayerData.CharControlCrouchCenterY, 0f);
            CharacterController.height = PlayerData.CharControlCrouchHeight;
        }
        else
        {
            CharacterController.center = new(0f, PlayerData.CharControlDefaultCenterY, 0f);
            CharacterController.height = PlayerData.CharControlDefaultHeight;
        }
    }
}
