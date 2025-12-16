using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    public abstract class BaseAxisMovement : BaseMovement, IUpdateState, IEnterable, IExitable
    {
        protected IAxisProvider inputAxisProvider;

        public bool CanEnter => inputAxisProvider.Axis != Vector2.zero;

        public bool CanExit => inputAxisProvider.Axis == Vector2.zero;

        protected BaseAxisMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
            Debug.Log("BaseAxisMovement Create");
        }

        [Inject]
        public void Construct(IAxisProvider inputAxisProvider)
        {
            Debug.Log("BaseAxisMovement Constructed");
            this.inputAxisProvider = Utils.Extensions.AssignWithNullCheck(inputAxisProvider);
        }

        public void Update()
        {
            OnMove(inputAxisProvider.Axis);
        }
        protected abstract void OnMove(Vector2 axis);
    }
}