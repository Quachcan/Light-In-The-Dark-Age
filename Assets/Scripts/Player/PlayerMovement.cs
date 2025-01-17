using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerControl _playerControl;
        private CharacterController _characterController;
        private Animator _animator;
        
        private Vector3 _currentMovementInput;
        private Vector3 _currentMovement;
        [SerializeField]
        private LayerMask whatIsGround;
        
        private bool _isMovementPressed;
        private bool _isMouseHeld;
        private bool _isSprinting;
        
        [SerializeField]
        private float rotationSpeed;
        
        [SerializeField]
        private float movementSpeed;
        private float _defaultMovementSpeed;
        [SerializeField]
        private float reducedMovementSpeed;
        [SerializeField]
        private float sprintSpeed;
        

        private void Awake()
        {
            _playerControl = new PlayerControl();
        }
        public void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            
            AnimationManager.Instance.InitializeAnimator(
                _animator, "isRunning", "isWalking", "isSprinting", "isStrafing", "XInput", "YInput");
            
            _playerControl.Player.Move.started += OnMovementInput;
            _playerControl.Player.Move.canceled += OnMovementInput;
            _playerControl.Player.Move.performed += OnMovementInput;
            
            _playerControl.Player.MouseClick.started += ctx => OnMouseDown();
            _playerControl.Player.MouseClick.canceled += ctx => OnMouseUp();

            _playerControl.Player.Sprint.started += ctx => OnSprinting();
            _playerControl.Player.Sprint.canceled += ctx => OffSprinting();
            
            _defaultMovementSpeed = movementSpeed;
        }

        private void Update()
        {
            AdjustMovementSpeed();
            HandleRotation();
            HandleMovement();
            HandleAnimation();
        }
        
        private void OnMovementInput(InputAction.CallbackContext ctx)
        {
            _currentMovementInput = ctx.ReadValue<Vector2>();
            _currentMovement.x = _currentMovementInput.x;
            _currentMovement.z = _currentMovementInput.y;
            _isMovementPressed =_currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        }

        private void OnMouseDown()
        {
            _isMouseHeld = true;
        }

        private void OnMouseUp()
        {
            _isMouseHeld = false;
        }

        private void OnSprinting()
        {
            _isSprinting = true;
        }

        private void OffSprinting()
        {
            _isSprinting = false;
        }

        private void HandleMovement()
        {
            // if (_isMovementPressed)
            // {
            //     Vector3 movement = _currentMovement.normalized;
            //     
            //     if (_isMouseHeld)
            //     {
            //         movement = transform.right * _currentMovement.x + transform.forward * _currentMovement.z;
            //     }
            //
            //     _characterController.Move(movement * (movementSpeed * Time.deltaTime));
            //     
            // }
            
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
                
            cameraForward.y = 0;
            cameraRight.y = 0;
                
            cameraForward.Normalize();
            cameraRight.Normalize();
                
            Vector3 movement = (cameraRight * _currentMovement.x + cameraForward * _currentMovement.z).normalized;
            
            _characterController.Move(movement * (movementSpeed * Time.deltaTime));
        }

        private void HandleRotation()
        {
            // if (_isMouseHeld)
            // {
            //     Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsGround))
            //     {
            //         
            //         Vector3 targetPosition = hit.point;
            //         targetPosition.y = transform.position.y;
            //         
            //         Vector3 lookDirection = (targetPosition - transform.position).normalized;
            //         if (lookDirection.sqrMagnitude > 0.01f)
            //         {
            //             Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            //             transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            //         }
            //     }
            // }
            // else if (_isMovementPressed)
            // {
            //     Vector3 direction = _currentMovement;
            //     if (direction.sqrMagnitude > 0.01f)
            //     {
            //         Quaternion rotation = Quaternion.LookRotation(direction);
            //         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            //     }
            // }

            if (_isMovementPressed)
            {
                Vector3 cameraForward = Camera.main.transform.forward;
                
                cameraForward.y = 0;
                
                cameraForward.Normalize();
                

                if (cameraForward.sqrMagnitude > 0.01f)
                {
                    Quaternion rotation = Quaternion.LookRotation(cameraForward);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            }
        }

        private void AdjustMovementSpeed()
        {
            if (_isSprinting && !_isMouseHeld)
            {
                movementSpeed = sprintSpeed;
            }
            else if (_isMouseHeld)
            {
                movementSpeed = reducedMovementSpeed;
            }
            else
            {
                movementSpeed = _defaultMovementSpeed;   
            }
        }

        private void HandleAnimation()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();
            
            Vector3 movement = cameraRight * _currentMovement.x + cameraForward * _currentMovement.z;

            float moveX = Vector3.Dot(movement, transform.right);
            float moveY = Vector3.Dot(movement, transform.forward);
            
            AnimationManager.Instance.SetFloat(_animator, "XInput", moveX);
            AnimationManager.Instance.SetFloat(_animator, "YInput", moveY);
            
            //bool isWalking = Mathf.Approximately(movementSpeed, reducedMovementSpeed) && _isMovementPressed;
            bool isRunning = Mathf.Approximately(movementSpeed, _defaultMovementSpeed) && _isMovementPressed;
            bool isSprinting = Mathf.Approximately(movementSpeed, sprintSpeed) && _isMovementPressed;
            bool isStrafing = _isMouseHeld && _isMovementPressed;
            
            //AnimationManager.Instance.SetBool(_animator, "isWalking", isWalking);
            AnimationManager.Instance.SetBool(_animator, "isRunning", isRunning);
            AnimationManager.Instance.SetBool(_animator, "isSprinting", isSprinting);
            AnimationManager.Instance.SetBool(_animator, "isStrafing", isStrafing);
            
        }

        private void OnEnable()
        {
            _playerControl.Enable();
        }

        private void OnDisable()
        {
            _playerControl.Disable();
        }
        
    }
}
