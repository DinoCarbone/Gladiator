using System;
using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class EmptyIdleState : BaseIdle, IEnterState, IExitState
    {
        /// <summary>
        /// Создаёт состояние пустого простоя.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        public EmptyIdleState(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        /// <summary>Всегда можно войти в пустое состояние.</summary>
        public bool CanEnter => true;

        /// <summary>Выход из пустого состояния недоступен.
        ///  Оно может прерваться только более важным состоянием, поэтому нужно давать этому состоянию самый низкий приоретет.</summary>
        public bool CanExit => false;

        /// <summary>Событие входа в состояние.</summary>
        public event Action OnEnter;

        /// <summary>Событие выхода из состояния.</summary>
        public event Action OnExit;

        /// <summary>Вызывает событие входа.</summary>
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        /// <summary>Вызывает событие выхода.</summary>
        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}