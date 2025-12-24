using System;
using System.Collections.Generic;
using Core.Services.States;

namespace Core.Behaviors.States
{
    public abstract class BaseIncompatible : IIncompatibleStates
    {
        /// <summary>Список типов состояний, несовместимых с текущим.</summary>
        public IReadOnlyList<Type> IncompatibleStates { get; private set; } = new List<Type>();

        /// <summary>Инициализирует несовместимые типы состояний.</summary>
        public BaseIncompatible(List<Type> incompatibleStates)
        {
            IncompatibleStates = incompatibleStates;
        }
    }
}