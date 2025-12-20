using System.Collections.Generic;
using Core.Services;
using Core.Services.States;
using Data.Serialization;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Animations
{
    public class ObserverAnimatorProvider : BaseAnimationProvider
    {
        private ObserverAnimator observerAnimator;
        Animator animator;
        public ObserverAnimatorProvider(Animator animator, List<AnimationStateTypeData> templateAnimationStates) : base(animator, templateAnimationStates)
        {
            this.animator = animator;
        }
        [Inject]
        private void Construct(ITickableService tickableService)
        {
            observerAnimator = new ObserverAnimator(tickableService, animator);
        }
        protected override void OnEnterState(AnimationStateEnterData enterData)
        {
            base.OnEnterState(enterData);
            if(enterData.EnterState is IExitActivator)
            {
                observerAnimator.AddNotifiable(enterData);
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            observerAnimator.Dispose();
            observerAnimator = null;
            animator = null;
        }
    }
}