using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AnimationManager
    {
        private static AnimationManager _instance;
        public static AnimationManager Instance => _instance ??= new AnimationManager();
        
        private readonly Dictionary<Animator, Dictionary<string, int>> _animatorHashes = new Dictionary<Animator, Dictionary<string, int>>();
        
        public void InitializeAnimator(Animator animator, params string[] parameters)
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

        public void SetBool(Animator animator, string parameter, bool value)
        {
            if (_animatorHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetBool(hash, value);
            }
        }

        public void SetTrigger(Animator animator, string parameter)
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
