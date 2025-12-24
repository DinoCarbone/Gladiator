using UnityEngine;

namespace Core.Behaviors.Animations
{
    /// <summary>
    /// Сервис проигрывания анимаций через <see cref="Animator"/> с поддержкой замены клипа и кроссфейда.
    /// </summary>
    public class AnimationPlayService : IAnimationPlayService
    {
        private Animator animator;

        /// <summary>Создаёт сервис для указанного <see cref="Animator"/>.</summary>
        public AnimationPlayService(Animator animator)
        {
            this.animator = animator;
        }

        /// <summary>
        /// Проигрывает анимацию по имени состояния, при необходимости подставляя <paramref name="clip"/>.
        /// </summary>
        /// <param name="stateName">Имя состояния в Animator.</param>
        /// <param name="clip">Альтернативный клип для подстановки (опционально).</param>
        /// <param name="blendTime">Время смешивания (crossfade).</param>
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
                float transitionProgress = animator.GetAnimatorTransitionInfo(0).normalizedTime;
                float remainingTime = 0.1f * (1f - transitionProgress); // Оставшееся время

                animator.CrossFade(stateName, remainingTime);
            }
            else animator.CrossFade(stateName, blendTime);
        }

        /// <summary>
        /// Заменяет клип для состояния в AnimatorOverrideController, если предоставлен клип.
        /// </summary>
        private void SetClipForState(string stateName, AnimationClip clip)
        {
            if (clip == null)
            {
                return;
            }

            AnimatorOverrideController overrideController = GetOrCreateOverrideController();
            overrideController[stateName] = clip;
        }

        /// <summary>
        /// Возвращает существующий OverrideController или создаёт новый на базе текущего контроллера.
        /// </summary>
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

        /// <summary>Возвращает имя текущего клипа на указанном слое, или null, если клипов нет.</summary>
        public string GetCurrentAnimationName(int layer = 0)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(layer);

            if (clipInfo.Length > 0)
            {
                return clipInfo[0].clip.name;
            }

            return null;
        }

    }
}