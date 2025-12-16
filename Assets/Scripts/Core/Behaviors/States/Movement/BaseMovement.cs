using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class BaseMovement : BaseIncompatible
    {
        public BaseMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
}