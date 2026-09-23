using System;
using System.Collections.Generic;

namespace Core.Behaviors.States.Movement
{
    public class BaseIdle : BaseIncompatible
    {
        public BaseIdle(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
}