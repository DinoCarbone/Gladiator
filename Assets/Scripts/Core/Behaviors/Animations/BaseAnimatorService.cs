using UnityEngine;

namespace Core.Behaviors.Animations
{
    public class BaseAnimatorService
    {
        private Animator animator;
        private AnimationClip currentClip;
        private string currentStateName;

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
            if (clip == currentClip && stateName == currentStateName)
            {
                animator.Play(stateName);
                return;
            }

            animator.CrossFade(stateName, blendTime);

            currentClip = clip;
            currentStateName = stateName;
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