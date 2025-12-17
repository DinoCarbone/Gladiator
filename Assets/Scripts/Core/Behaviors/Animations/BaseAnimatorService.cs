using UnityEditor.Animations;
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
            if (clip == null || string.IsNullOrEmpty(stateName))
            {
                Debug.LogError("Clip или stateName не могут быть null!");
                return;
            }

            // Проверяем, не пытаемся ли воспроизвести ту же анимацию в том же состоянии
            if (clip == currentClip && stateName == currentStateName)
                return;

            // Устанавливаем клип в нужное состояние через AnimatorOverrideController
            SetClipForState(stateName, clip);

            // Плавно переключаемся на состояние
            animator.CrossFade(stateName, blendTime);

            // Сохраняем текущие значения
            currentClip = clip;
            currentStateName = stateName;
        }

        private void SetClipForState(string stateName, AnimationClip clip)
        {
            // Получаем или создаем AnimatorOverrideController
            AnimatorOverrideController overrideController = GetOrCreateOverrideController();

            // Устанавливаем клип для указанного состояния
            overrideController[stateName] = clip;
        }

        private AnimatorOverrideController GetOrCreateOverrideController()
        {
            // Если уже есть OverrideController - используем его
            if (animator.runtimeAnimatorController is AnimatorOverrideController existingOverride)
            {
                return existingOverride;
            }

            // Создаем новый OverrideController на основе текущего контроллера
            var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = overrideController;

            return overrideController;
        }

    }
}