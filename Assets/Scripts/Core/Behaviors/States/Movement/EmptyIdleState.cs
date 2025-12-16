using System;
using System.Collections.Generic;

namespace Core.Behaviors.States.Movement
{
    public class EmptyIdleState : BaseIdle
    {
        public EmptyIdleState(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
}