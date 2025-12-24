using System;
using System.Collections.Generic;

namespace Core.Behaviors.States.Movement
{
    public class BaseIdle : BaseIncompatible
    {
        /// <summary>
        /// Базовый тип для idle-состояний, содержит список несовместимых состояний.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        public BaseIdle(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
}