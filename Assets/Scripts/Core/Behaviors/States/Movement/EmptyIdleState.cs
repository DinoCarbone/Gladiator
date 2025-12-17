using System;
using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class EmptyIdleState : BaseIdle, IEnterState, IExitState
    {
        public EmptyIdleState(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        public bool CanEnter => true;

        public bool CanExit => false;

        public void Enter()
        {
            Debug.Log("EmptyIdleState Enter");
        }

        public void Exit()
        {
            Debug.Log("EmptyIdleState Exit");
        }
    }
}