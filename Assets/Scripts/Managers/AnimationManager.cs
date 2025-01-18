using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        private static AnimationManager _instance;

        [Header("References")]
        public Animator animator;
        public TwoBoneIKConstraint rightHandIK;
        public TwoBoneIKConstraint leftHandIK;

        [SerializeField]
        private RigBuilder _rigBuilder;
        
        private readonly Dictionary<Animator, Dictionary<string, int>> _animatorHashes = new Dictionary<Animator, Dictionary<string, int>>();

        private void Awake()
        {
            _rigBuilder = GetComponentInChildren<RigBuilder>();
        }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void InitializeAnimator(Animator animators, params string[] parameters)
        {
            if (!_animatorHashes.ContainsKey(animator))
            {
                var hashes = new Dictionary<string, int>();
                foreach (var parameter in parameters)
                {
                    hashes[parameter] = Animator.StringToHash(parameter);
                }
                _animatorHashes[animator] = hashes;
            }
        }

        public void SetBool(Animator targetAnimator, string parameter, bool value)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetBool(hash, value);
            }
        }

        public void SetTrigger(Animator targetAnimator, string parameter)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetTrigger(hash);
            }
        }

        public void SetFloat(Animator targetAnimator, string parameter, float value)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetFloat(hash, value);
            }
        }

        public void AssignHandIK(RightHandIKTarget rightHandTarget, LeftHandIKTarget leftHandTarget)
        {
            rightHandIK.data.target = rightHandTarget.transform;
            leftHandIK.data.target = leftHandTarget.transform;
            if(_rigBuilder == null)
            {
                Debug.Log("Null");
            }
            _rigBuilder.Build();
        }


        private void HandleAnimationValues(float horizontalMovement, float verticalMovement)
        {
            
        }
        
    }
}
