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
    
    private bool _isWalking;
    public bool IsWalking => _isWalking;

    [SerializeField]
    private bool _isAiming;
    public bool IsAiming => _isAiming;

    private void Awake()
    {
        _playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        _playerControl.Enable();

        //Movement
        _playerControl.Player.Move.started += OnMoveInput;
        _playerControl.Player.Move.canceled += OnMoveInput;
        _playerControl.Player.Move.performed += OnMoveInput;
        
        //Camera Input
        _playerControl.Player.Look.started += OnCameraInput;
        _playerControl.Player.Look.canceled += OnCameraInput;
        _playerControl.Player.Look.performed += OnCameraInput;
        
        //Sprint Input
        _playerControl.Player.Sprint.started += OnSprintInput;
        _playerControl.Player.Sprint.canceled += OnSprintInput;
        _playerControl.Player.Sprint.performed += OnSprintInput;

        //Walk Input
        _playerControl.Player.Walking.performed += OnWalkToggle;

        //Aim Input
        _playerControl.Action.Aim.started += OnAimInput;
        _playerControl.Action.Aim.canceled += OnAimInput;
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

        _playerControl.Action.Aim.started -= OnAimInput;
        _playerControl.Action.Aim.canceled -= OnAimInput;

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

    private void OnAimInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _isAiming = true;
        }
        else if (context.canceled)
        {
            _isAiming = false;
        }
    }
}
