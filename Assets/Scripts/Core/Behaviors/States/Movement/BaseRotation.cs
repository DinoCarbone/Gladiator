using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    public class BaseRotation : BaseIncompatible
    {
        public BaseRotation(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public abstract class BaseAxisRotation : BaseRotation, IUpdateState, IEnterState, IExitState
    {
        protected IAxisRotationProvider axisRotationProvider;

        public event Action OnEnter;
        public event Action OnExit;

        public bool CanEnter => true;

        public bool CanExit => false;

        protected BaseAxisRotation(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        [Inject]
        public void Construct(IAxisRotationProvider axisRotationProvider)
        {
            this.axisRotationProvider = Utils.Extensions.AssignWithNullCheck(axisRotationProvider);
        }

        public void Update()
        {
            OnRotation(axisRotationProvider.Rotation);
        }
        protected abstract void OnRotation(Quaternion rotation);

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