using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        private static AnimationManager _instance;
        
        public Animator animator;
        
        private readonly Dictionary<Animator, Dictionary<string, int>> _animatorHashes = new Dictionary<Animator, Dictionary<string, int>>();

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

        public void SetBool(Animator animators, string parameter, bool value)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetBool(hash, value);
            }
        }

        public void SetTrigger(Animator animators, string parameter)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetTrigger(hash);
            }
        }

        public void SetFloat(Animator animator, string parameter, float value)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetFloat(hash, value);
            }
        }

        private void HandleAnimationValues(float horizontalMovement, float verticalMovement)
        {
            
        }
        
    }
}
