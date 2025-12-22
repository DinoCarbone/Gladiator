using System;
using System.Collections.Generic;
using Core.Services.States;

namespace Core.Behaviors.States
{
    public abstract class BaseIncompatible : IIncompatibleStates
    {
         public IReadOnlyList<Type> IncompatibleStates {get; private set; } = new List<Type>();
         public BaseIncompatible(List<Type> incompatibleStates)
         {
             IncompatibleStates = incompatibleStates;
         }
    }
}