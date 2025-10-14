using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    // Zenject dependency injection. //
    private PlayerDataSheet _playerData;
    private SettingsHolder _settings;

    private PlayerBaseState _currentState;

    private PlayerWalkState _walkState;
    private PlayerRunState _runState;
    private PlayerCrouchState _crouchState;

    public PlayerDataSheet PlayerData => _playerData;
    public SettingsHolder Settings => _settings;

    [Inject]
    public void Construct(SettingsHolder settings, PlayerDataSheet playerDataSheet)
    {
        _settings = settings;
        _playerData = playerDataSheet;
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

            if (Input.GetKey(_settings.Data.ForwardKey)) _moveVector += transform.forward;
            if (Input.GetKey(_settings.Data.StrafeLeftKey)) _moveVector -= transform.right;
            if (Input.GetKey(_settings.Data.BackwardKey)) _moveVector -= transform.forward;
            if (Input.GetKey(_settings.Data.StrafeRightKey)) _moveVector += transform.right;

            // Chooses player speed based on Shift key input. -Shad //
            _currentPlayerSpeed = Input.GetKey(_settings.Data.RunKey) ? _playerData.RunSpeed : _playerData.WalkSpeed;
        }

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        _moveVector.Normalize();

        _smoothMoveVector = Vector3.SmoothDamp(_smoothMoveVector, _moveVector, ref _refVector, _playerData.MovementSmoothingTime);

        _characterController.Move(_smoothMoveVector * _currentPlayerSpeed * Time.deltaTime);

        _gravityVector.z = Mathf.Clamp(_gravityVector.z, 0f, _playerData.RunJumpSpeedMultiplier);
        _gravityVector.z += 0.5f * _playerData.Gravity * Time.deltaTime;

        // Gravity logic. Keeps the character grounded even if not midair. -Shad //
        if (!_characterController.isGrounded)
        {
            _gravityVector.y += _playerData.Gravity * Time.deltaTime;
        }
        else if(_gravityVector.y < 0f)
        {
            _gravityVector.y = -2f;
        }

        _characterController.Move(transform.TransformDirection(_gravityVector) * Time.deltaTime);

        // Triggers Jump() once. -Shad //
        if (_characterController.isGrounded && Input.GetKeyDown(_settings.Data.JumpKey)) Jump();

        // Crouching shouldn't behave like Jump(), like a trigger. Which is why it's like this. -Shad //
        HandleCrouch(Input.GetKey(_settings.Data.CrouchKey));
    }

    private void Jump()
    {
        _gravityVector.y = Mathf.Sqrt(2f * -_playerData.Gravity * _playerData.JumpHeight);

        if(_currentPlayerSpeed == _playerData.RunSpeed) _gravityVector.z = _playerData.RunSpeed * _playerData.RunJumpSpeedMultiplier;
    }

    private bool _hasSlided = false;
    private void HandleCrouch(bool canCrouch)
    {
        if (canCrouch)
        {
            if (!_hasSlided)
            {
                if(Input.GetKey(_settings.Data.RunKey))

                Slide();

                _hasSlided = true;
            }

            _characterController.center.Set(0f, _playerData.CharControlCrouchCenterY, 0f);
            _characterController.height = _playerData.CharControlCrouchHeight;
        }
        else
        {
            _characterController.center.Set(0f, _playerData.CharControlDefaultCenterY, 0f);
            _characterController.height = _playerData.CharControlDefaultHeight;            
        }
    }

    private float _slideTimer = 0f;
    private async void Slide()
    {
        _slideTimer = 0f;

        _gravityVector.z = _playerData.RunSpeed * _playerData.SlideSpeedMultiplier;

        while (_slideTimer < _playerData.SlideCooldown)
        {
            _slideTimer += Time.deltaTime;

            print("Resetting timer...");

            await Task.Yield();
        }

        _hasSlided = false;
    }

    private bool CanSlide()
    {
        return _characterController.isGrounded && _slideTimer > _playerData.SlideCooldown;
    }
}
