using System;
using Core.Services;
using Core.Services.States;
using Data.Serialization;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Animations
{
    /// <summary>
    /// Наблюдает за окончанием проигрывания текущей анимации и уведомляет состояние о завершении (через IExitActivator).
    /// Подписывается на тик-сервис и отслеживает оставшееся время анимации с учётом blend time.
    /// </summary>
    public class AnimationEndNotifier : IAnimationEndNotifier, IDisposable
    {
        private readonly ITickableService tickableService;
        private readonly Animator animator;
        private AnimationStateEnterData currentAnimationStateData;

        /// <summary>
        /// Создаёт наблюдатель окончания анимации и подписывается на <see cref="ITickableService.OnTick"/>.
        /// </summary>
        /// <param name="tickableService">Сервис тиков, используемый для обновлений.</param>
        /// <param name="animator">Animator, из которого читается состояние анимации.</param>
        public AnimationEndNotifier(ITickableService tickableService, Animator animator)
        {
            this.tickableService = Extensions.AssignWithNullCheck(tickableService);
            this.animator = Extensions.AssignWithNullCheck(animator);
            tickableService.OnTick += OnTick;
        }

        /// <summary>
        /// Регистрирует новые данные анимации для отслеживания. Если уже зарегистрирована анимация — сначала вызывает завершение для неё.
        /// </summary>
        /// <param name="notifiableData">Данные текущей анимации для отслеживания.</param>
        public void AddNotifiable(AnimationStateEnterData notifiableData)
        {
            NotifyAnimationEnded();
            currentAnimationStateData = notifiableData;
        }

        private void OnTick()
        {
            if (currentAnimationStateData == null) return;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(currentAnimationStateData.StateName))
            {
                float animationLength = stateInfo.length;
                float timeLeft = animationLength * (1f - stateInfo.normalizedTime);
                float warningTime = currentAnimationStateData.BlendTime;

                if (timeLeft < warningTime && !animator.IsInTransition(0))
                {
                    NotifyAnimationEnded();
                }
            }
        }

        private void NotifyAnimationEnded()
        {
            if (currentAnimationStateData == null) return;

            if (currentAnimationStateData.EnterState is IExitActivator exitActivator)
            {
                exitActivator.ActivateExit();
            }
            else Debug.LogWarning("State doesn't implement IExitActivator");

            currentAnimationStateData = null;
        }

        /// <summary>Отписывается от сервиса тиков.</summary>
        public void Dispose()
        {
            tickableService.OnTick -= OnTick;
        }
    }
}