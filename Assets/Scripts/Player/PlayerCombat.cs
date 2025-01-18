using Managers;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private AnimationManager _animationManager;
        private Animator _animator;
        private InputManager _inputManager;

        [Header("Aim Constraints")]
        public MultiAimConstraint spine01;
        public MultiAimConstraint spine02;
        public MultiAimConstraint head;

        [SerializeField]
        private bool _isAiming;

        public void Awake()
        {
            _animationManager = GetComponent<AnimationManager>();
            _animator = GetComponent<Animator>();
            _inputManager = GetComponent<InputManager>();
            _animationManager.InitializeAnimator(_animator, "isAiming");
        }
        private void Update()
        {
            HandleAnimations();
        }

        private void HandleAnimations()
        {
            Vector2 moveInput = _inputManager.MoveInput;
            _isAiming = _inputManager.IsAiming;
    
            if(_isAiming )
            {
                spine01.weight = 0.7f;
                spine02.weight = 0.7f;
                head.weight = 0.7f;
            }
            else
            {
                spine01.weight = 0;
                spine02.weight = 0;
                head.weight = 0;
            }

            _animationManager.SetBool(_animator, "isAiming", _isAiming);
            
        }
    }
}
