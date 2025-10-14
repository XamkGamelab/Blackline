using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
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
    public PlayerJumpState JumpState;
    [HideInInspector]
    public PlayerFallingState FallingState;

    // Public, but hidden references. //
    public CharacterController CharacterController => _characterController;
    [HideInInspector]
    public Vector3 MoveVector;
    [HideInInspector]
    public Vector3 Velocity;

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
        JumpState = new PlayerJumpState(this);
        FallingState = new PlayerFallingState(this);

        UpdateState(WalkState);
    }

    private void Update()
    {
        CurrentState.HandleInput();
        CurrentState.HandleUpdate();

        print(CurrentState);
        print(Velocity);
    }

    public void UpdateState(PlayerBaseState newState)
    {
        if (CurrentState != null) CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
