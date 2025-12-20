using UnityEngine;

namespace Core.Behaviors.Animations
{
    public class BaseAnimatorService
    {
        private Animator animator;

        public BaseAnimatorService(Animator animator)
        {
            this.animator = animator;
        }

        public void Play(string stateName, AnimationClip clip, float blendTime = 0.2f)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                Debug.LogError("StateName не могут быть null!");
                return;
            }
            
            SetClipForState(stateName, clip);
            if (animator.IsInTransition(0))
            {
                animator.Play(stateName);
            }
            else animator.CrossFade(stateName, blendTime);
        }

        private void SetClipForState(string stateName, AnimationClip clip)
        {
            if(clip == null)
            {
                return;
            }
            AnimatorOverrideController overrideController = GetOrCreateOverrideController();

            overrideController[stateName] = clip;
        }

        private AnimatorOverrideController GetOrCreateOverrideController()
        {
            if (animator.runtimeAnimatorController is AnimatorOverrideController existingOverride)
            {
                return existingOverride;
            }

            var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = overrideController;
            return overrideController;
        }

    }
}