using System;
using System.Collections.Generic;
using Core.Services.States;
using Data.Dto;
using UnityEngine;
using Zenject;
using Utils;
using Data.ScriptableObjects.Animatios;
using Core.Behaviors.Interaction;

namespace Core.Behaviors.Animations
{
    /// <summary>
    /// Расширение <see cref="AnimationTransitionHandler"/>, которое дополнительно ретранслирует события анимаций
    /// и регистрирует наблюдатель завершения анимации для состояний, реализующих <see cref="IExitActivator"/>.
    /// </summary>
    public class AnimationTransitionNotifier : AnimationTransitionHandler
    {
        private IAnimationEndNotifier observerEnding;
        private IAnimationEventsNotifier observerEvents;
        private IAnimationEventReceiveService animationEventListener;

        public AnimationTransitionNotifier(Animator animator, List<AnimationStateTypeData> templateAnimationStates) :
            base(animator, templateAnimationStates)
        {
        }

        /// <summary>
        /// Инъекция фабрик и слушателя событий анимаций.
        /// </summary>
        [Inject]
        private void Construct(IAnimationEndNotifierFactory animationEndNotifierFactory,
            IAnimationEventsNotifierFactory animationEventsNotifierFactory, IAnimationEventReceiveService animationEventListener)
        {
            observerEnding = Extensions.AssignWithNullCheck(animationEndNotifierFactory.Create(animator));
            observerEvents = Extensions.AssignWithNullCheck(animationEventsNotifierFactory.Create(animator));
            this.animationEventListener = Extensions.AssignWithNullCheck(animationEventListener);
        }

        protected override void OnEnterState(AnimationStateEnterData enterData)
        {
            base.OnEnterState(enterData);
            if (enterData.EnterState is IExitActivator)
            {
                observerEnding.AddNotifiable(enterData);
            }
        }

        protected override void Subscribe()
        {
            observerEvents.OnAnimationEvent += NotifySubject;
            base.Subscribe();
        }

        private void NotifySubject(AnimationEventSO animationEvent)
        {
            animationEventListener.OnAnimationEvent(animationEvent);
        }

        protected override void Unsubscribe()
        {
            observerEvents.OnAnimationEvent -= NotifySubject;
            base.Unsubscribe();
        }

        /// <summary>Освобождает ресурсы и отписывает наблюдателей.</summary>
        public override void Dispose()
        {
            base.Dispose();
            if (observerEnding is IDisposable disposable) disposable.Dispose();
            observerEnding = null;
        }
    }
}