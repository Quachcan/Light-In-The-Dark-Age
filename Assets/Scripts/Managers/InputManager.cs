using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControl _playerControl;

    private Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;
    
    private Vector2 _lookInput;
    public Vector2 LookInput => _lookInput;
    
    private bool _isSprinting;
    public bool IsSprinting => _isSprinting;
    
    public bool _isWalking;
    public bool IsWalking => _isWalking;

    private void Awake()
    {
        _playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        _playerControl.Enable();

        _playerControl.Player.Move.started += OnMoveInput;
        _playerControl.Player.Move.canceled += OnMoveInput;
        _playerControl.Player.Move.performed += OnMoveInput;
        
        _playerControl.Player.Look.started += OnCameraInput;
        _playerControl.Player.Look.canceled += OnCameraInput;
        _playerControl.Player.Look.performed += OnCameraInput;
        
        _playerControl.Player.Sprint.started += OnSprintInput;
        _playerControl.Player.Sprint.canceled += OnSprintInput;
        _playerControl.Player.Sprint.performed += OnSprintInput;

        _playerControl.Player.Walking.performed += OnWalkToggle;
    }

    private void OnDisable()
    {
        
        _playerControl.Player.Move.started -= OnMoveInput;
        _playerControl.Player.Move.canceled -= OnMoveInput;
        _playerControl.Player.Move.performed -= OnMoveInput;
        
        _playerControl.Player.Look.started -= OnCameraInput;
        _playerControl.Player.Look.canceled -= OnCameraInput;
        _playerControl.Player.Look.performed -= OnCameraInput;
        
         _playerControl.Player.Sprint.started -= OnSprintInput;
        _playerControl.Player.Sprint.canceled -= OnSprintInput;
        _playerControl.Player.Sprint.performed -= OnSprintInput;
        
        _playerControl.Player.Walking.performed -= OnWalkToggle;
        
        _playerControl.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnCameraInput(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void OnSprintInput(InputAction.CallbackContext context)
    {
        float sprintValue = context.ReadValue<float>();
        if (sprintValue >=0.5f)
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }

    private void OnWalkToggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isWalking = !_isWalking;
        }
    }
}
