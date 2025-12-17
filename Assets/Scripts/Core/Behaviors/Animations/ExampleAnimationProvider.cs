using System;
using Core.Behaviors.States.Movement;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Animations
{
    public class ExampleAnimationProvider : Providers.IProvider, IDisposable
    {
        private EmptyIdleState idleState;
        private BaseAxisMovement movementState;
        private AnimationClip idleAimation;
        private AnimationClip moveAimation;

        private readonly BaseAnimatorService animatorService;
        public ExampleAnimationProvider(Animator animator, AnimationClip idleAimation, AnimationClip moveAimation)
        {
            animatorService = new BaseAnimatorService(animator);
            this.idleAimation = idleAimation;
            this.moveAimation = moveAimation;
            Debug.Log("ExampleAnimationProvider Created");
        }
        [Inject]
        private void Construct(EmptyIdleState idleState, BaseAxisMovement movementState)
        {
            Debug.Log("ExampleAnimationProvider Constructed");
            this.idleState = idleState;
            this.movementState = movementState;
            this.idleState.OnEnter += OnEnterIdleState;
            this.movementState.OnEnter += OnEnterMovementState;
        }

        private void OnEnterMovementState()
        {
            animatorService.Play("Move",moveAimation);
        }

        private void OnEnterIdleState()
        {
            animatorService.Play("Idle",idleAimation);
        }

        public void Dispose()
        {
            idleState.OnEnter -= OnEnterIdleState;
            movementState.OnEnter -= OnEnterMovementState;
            Debug.Log("ExampleAnimationProvider Disposed");
        }
    }
}