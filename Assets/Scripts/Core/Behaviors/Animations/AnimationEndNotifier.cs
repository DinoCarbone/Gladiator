using System;
using Core.Services;
using Core.Services.States;
using Data.Serialization;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Animations
{
    public class AnimationEndNotifier : IAnimationEndNotifier, IDisposable
    {
        private readonly ITickableService tickableService;
        private readonly Animator animator;
        private AnimationStateEnterData currentAnimationStateData;
        public AnimationEndNotifier(ITickableService tickableService, Animator animator)
        {
            this.tickableService = Extensions.AssignWithNullCheck(tickableService);
            this.animator = Extensions.AssignWithNullCheck(animator);
            tickableService.OnTick += OnTick;
        }
        public void AddNotifiable(AnimationStateEnterData notifiableData)
        {
            NotifyAnimationEnded();
            currentAnimationStateData = notifiableData;
        }

        private void OnTick()
        {
            if(currentAnimationStateData == null) return;

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
            if(currentAnimationStateData == null) return;

            if(currentAnimationStateData.EnterState is IExitActivator exitActivator)
            {
                exitActivator.ActivateExit();
            }
            else Debug.LogWarning("State doesn't implement IExitActivator");

            currentAnimationStateData = null;
        }

        public void Dispose()
        {
            tickableService.OnTick -= OnTick;
        }
    }
}