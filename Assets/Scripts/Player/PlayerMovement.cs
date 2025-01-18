using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerControl _playerControl;
        private Animator _animator;
        private InputManager _inputManager;
        private Rigidbody _rigidbody;
        private AnimationManager _animationManager;
        
        [SerializeField]
        private LayerMask whatIsGround;

        public Transform playerCamera;
        
        private bool _isRunning;
        private bool _isSprinting;
        public bool _isWalking;
        
        [SerializeField]
        private float rotationSpeed;
        private Quaternion _targetRotation;
        private Quaternion _playerRotation;
        
        [SerializeField]
        private float movementSpeed;
        private float _defaultMovementSpeed;
        [SerializeField]
        private float reducedMovementSpeed;
        [SerializeField]
        private float sprintSpeed;
        [SerializeField]
        private float walkSpeed;

        private float _targetXValue;
        private float _targetYValue;


        private void Awake()
        {
            _playerControl = new PlayerControl();
        }
        public void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _inputManager = GetComponent<InputManager>();
            _rigidbody = GetComponent<Rigidbody>();
            _animationManager = GetComponentInChildren<AnimationManager>();
            
            _animationManager.InitializeAnimator(_animator, "XInput", "YInput", "isWalking", "isAiming");
            
            _defaultMovementSpeed = movementSpeed;
        }

        private void Update()
        {
            HandleAnimation();
        }

        private void FixedUpdate()
        {
            HandleRotation();
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 moveInput = _inputManager.MoveInput;
            bool _isAiming = _inputManager.IsAiming;
            _isRunning = (moveInput.sqrMagnitude > 0.01f);
            _isWalking = _inputManager.IsWalking;
            _isSprinting = _inputManager.IsSprinting;

            if (_isWalking || moveInput.y <0f || _isAiming)
            {
                movementSpeed = walkSpeed;
            }
            else if (_isSprinting)
            {
                movementSpeed = sprintSpeed;
            }
            else
            {
                movementSpeed = _defaultMovementSpeed;
            }
            
            if (_isRunning)
            {
                Vector3 cameraForward = playerCamera.transform.forward;
                Vector3 cameraRight = playerCamera.transform.right;
                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();
                
                Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;
                Vector3 targetPosition = _rigidbody.position + moveDirection * (movementSpeed * Time.deltaTime);
                
                _rigidbody.MovePosition(targetPosition);
            }
        }

        private void HandleRotation()
        {
            if(_inputManager.IsAiming)
            {
                _targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
                _playerRotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = _playerRotation;
            }
            else
            {
                _targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
                _playerRotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

                if (_inputManager.MoveInput.x != 0 || _inputManager.MoveInput.y != 0)
                {
                    transform.rotation = _playerRotation;
                }
            }
        }
        

        private void HandleAnimation()
        {
            Vector2 moveInput = _inputManager.MoveInput;
            float xValue = moveInput.x;
            float yValue = moveInput.y;
            bool walking = _inputManager.IsWalking;
            bool _isAiming = _inputManager.IsAiming;

            if(xValue != 0 || yValue != 0 || _isAiming)
            {
                _animationManager.rightHandIK.weight = 0f;
                _animationManager.leftHandIK.weight = 0f;
            }
            else
            {
                _animationManager.rightHandIK.weight = 1f;
                _animationManager.leftHandIK.weight = 1f;
            }

            if (_inputManager.IsSprinting && yValue > 0)
            {
                yValue = 2f;
            }



            _targetXValue = Mathf.Lerp(_targetXValue, xValue, 10f *Time.deltaTime);
            _targetYValue = Mathf.Lerp(_targetYValue, yValue, 10f * Time.deltaTime);
            
            _animationManager.SetBool(_animator, "isWalking", walking);
            _animationManager.SetFloat(_animator, "XInput", _targetXValue);
            _animationManager.SetFloat(_animator, "YInput", _targetYValue);
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
