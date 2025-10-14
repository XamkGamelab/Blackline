using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    // Zenject dependency injection. //
    [HideInInspector]
    public PlayerDataSheet PlayerData;
    public SettingsHolder Settings;

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

    [Inject]
    public void Construct(SettingsHolder settings, PlayerDataSheet playerDataSheet)
    {
        Settings = settings;
        PlayerData = playerDataSheet;
    }

    private void Awake()
    {
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        CrouchState = new PlayerCrouchState(this);
        JumpState = new PlayerJumpState(this);
        FallingState = new PlayerFallingState(this);
    }

    private void Update()
    {
        MovementLogic();
    }

    private Vector3 _moveVector, _gravityVector, _smoothMoveVector, _refVector;
    private float _currentPlayerSpeed;
    private void MovementLogic()
    {
        if (_characterController.isGrounded)
        {
            _moveVector = Vector3.zero;

            if (Input.GetKey(Settings.Data.ForwardKey)) _moveVector += transform.forward;
            if (Input.GetKey(Settings.Data.StrafeLeftKey)) _moveVector -= transform.right;
            if (Input.GetKey(Settings.Data.BackwardKey)) _moveVector -= transform.forward;
            if (Input.GetKey(Settings.Data.StrafeRightKey)) _moveVector += transform.right;

            // Chooses player speed based on Shift key input. -Shad //
            _currentPlayerSpeed = Input.GetKey(Settings.Data.RunKey) ? PlayerData.RunSpeed : PlayerData.WalkSpeed;
        }

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        _moveVector.Normalize();

        _smoothMoveVector = Vector3.SmoothDamp(_smoothMoveVector, _moveVector, ref _refVector, PlayerData.MovementSmoothingTime);

        _characterController.Move(_smoothMoveVector * _currentPlayerSpeed * Time.deltaTime);

        _gravityVector.z = Mathf.Clamp(_gravityVector.z, 0f, PlayerData.RunJumpSpeedMultiplier);
        _gravityVector.z += 0.5f * PlayerData.Gravity * Time.deltaTime;

        // Gravity logic. Keeps the character grounded even if not midair. -Shad //
        if (!_characterController.isGrounded)
        {
            _gravityVector.y += PlayerData.Gravity * Time.deltaTime;
        }
        else if(_gravityVector.y < 0f)
        {
            _gravityVector.y = -2f;
        }

        _characterController.Move(transform.TransformDirection(_gravityVector) * Time.deltaTime);

        // Triggers Jump() once. -Shad //
        if (_characterController.isGrounded && Input.GetKeyDown(Settings.Data.JumpKey)) Jump();

        // Crouching shouldn't behave like Jump(), like a trigger. Which is why it's like this. -Shad //
        HandleCrouch(Input.GetKey(Settings.Data.CrouchKey));
    }

    private void Jump()
    {
        _gravityVector.y = Mathf.Sqrt(2f * -PlayerData.Gravity * PlayerData.JumpHeight);

        if(_currentPlayerSpeed == PlayerData.RunSpeed) _gravityVector.z = PlayerData.RunSpeed * PlayerData.RunJumpSpeedMultiplier;
    }

    private bool _hasSlided = false;
    private void HandleCrouch(bool canCrouch)
    {
        if (canCrouch)
        {
            if (!_hasSlided)
            {
                if(Input.GetKey(Settings.Data.RunKey))

                Slide();

                _hasSlided = true;
            }

            _characterController.center.Set(0f, PlayerData.CharControlCrouchCenterY, 0f);
            _characterController.height = PlayerData.CharControlCrouchHeight;
        }
        else
        {
            _characterController.center.Set(0f, PlayerData.CharControlDefaultCenterY, 0f);
            _characterController.height = PlayerData.CharControlDefaultHeight;            
        }
    }

    private float _slideTimer = 0f;
    private async void Slide()
    {
        _slideTimer = 0f;

        _gravityVector.z = PlayerData.RunSpeed * PlayerData.SlideSpeedMultiplier;

        while (_slideTimer < PlayerData.SlideCooldown)
        {
            _slideTimer += Time.deltaTime;

            print("Resetting timer...");

            await Task.Yield();
        }

        _hasSlided = false;
    }

    public void UpdateState(PlayerBaseState newState)
    {
        if (CurrentState != null) CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
