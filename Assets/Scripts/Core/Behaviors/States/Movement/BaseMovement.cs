using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    public class BaseMovement : BaseIncompatible
    {
        /// <summary>
        /// Базовый класс для состояний движения.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        public BaseMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public abstract class BaseAxisMovement : BaseMovement, IUpdateState, IEnterState, IExitState
    {
        protected IAxisMovementProvider inputAxisProvider;

        public event Action OnEnter;
        public event Action OnExit;

        public bool CanEnter => inputAxisProvider.IsHandle;

        public bool CanExit => !inputAxisProvider.IsHandle;
        /// <summary>
        /// Конструктор для состояний, основанных на осевом вводе.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        protected BaseAxisMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        /// <summary>
        /// Инъекция провайдера осевого движения.
        /// </summary>
        /// <param name="inputAxisProvider">Провайдер осевого ввода.</param>
        [Inject]
        public void Construct(IAxisMovementProvider inputAxisProvider)
        {
            this.inputAxisProvider = Utils.Extensions.AssignWithNullCheck(inputAxisProvider);
        }

        /// <summary>
        /// Обновление состояния — прокидывает текущую ось в обработчик движения.
        /// </summary>
        public void Update()
        {
            OnMove(inputAxisProvider.Axis);
        }

        /// <summary>
        /// Обрабатывает движение по вектору оси.
        /// </summary>
        /// <param name="axis">Текущий вектор осевого ввода.</param>
        protected abstract void OnMove(Vector2 axis);

        /// <summary>Вызывается при входе в состояние.</summary>
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        /// <summary>Вызывается при выходе из состояния.</summary>
        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}