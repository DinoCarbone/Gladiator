using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    public class BaseMovement : BaseIncompatible
    {
        public BaseMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public abstract class BaseAxisMovement : BaseMovement, IUpdateState, IEnterState, IExitState
    {
        protected IAxisMovementProvider inputAxisProvider;

        public event Action OnEnter;
        public event Action OnExit;

        public bool CanEnter => inputAxisProvider.Axis != Vector2.zero;

        public bool CanExit => inputAxisProvider.Axis == Vector2.zero;

        protected BaseAxisMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        [Inject]
        public void Construct(IAxisMovementProvider inputAxisProvider)
        {
            this.inputAxisProvider = Utils.Extensions.AssignWithNullCheck(inputAxisProvider);
        }

        public void Update()
        {
            OnMove(inputAxisProvider.Axis);
        }
        protected abstract void OnMove(Vector2 axis);

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}