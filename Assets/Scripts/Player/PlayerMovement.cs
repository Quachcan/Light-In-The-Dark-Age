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
        
        private bool _isMovementPressed;
        
        [SerializeField]
        private float rotationSpeed;
        
        [SerializeField]
        private float movementSpeed;

        private void Awake()
        {
            _playerControl = new PlayerControl();
        }
        public void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            
            AnimationManager.Instance.InitializeAnimator(_animator, "isMoving");
            
            _playerControl.Player.Move.started += OnMovementInput;
            _playerControl.Player.Move.canceled += OnMovementInput;
            _playerControl.Player.Move.performed += OnMovementInput;
        }

        private void Update()
        {
            if (_isMovementPressed)
            {
                HandleRotation();
            }
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

        private void HandleMovement()
        {
            Vector3 movement = _currentMovement;
            movement.y = -1f;
            _characterController.Move(movement * (movementSpeed * Time.deltaTime));
        }

        private void HandleRotation()
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = _currentMovement.x;
            positionToLookAt.y = 0;
            positionToLookAt.z = _currentMovement.z;

            if (positionToLookAt.sqrMagnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(positionToLookAt);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }

        private void HandleAnimation()
        {
            AnimationManager.Instance.SetBool(_animator, "isMoving", _isMovementPressed);
        }

        private void OnEnable()
        {
            _playerControl.Enable();
        }

        private void OnDisable()
        {
            _playerControl.Disable();
        }
        

        // Update is called once per frame
    }
}
