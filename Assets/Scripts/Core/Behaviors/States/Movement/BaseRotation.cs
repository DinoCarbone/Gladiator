using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    public class BaseRotation : BaseIncompatible
    {
        /// <summary>
        /// Базовый тип для состояний вращения.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        public BaseRotation(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public abstract class BaseAxisRotation : BaseRotation, IUpdateState, IEnterState, IExitState
    {
        protected IAxisRotationProvider axisRotationProvider;

        public event Action OnEnter;
        public event Action OnExit;

        public bool CanEnter => true;

        public bool CanExit => false;
        /// <summary>
        /// Конструктор базового вращения на основе осевого провайдера.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        protected BaseAxisRotation(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        /// <summary>
        /// Инъекция провайдера осевого вращения.
        /// </summary>
        /// <param name="axisRotationProvider">Провайдер, предоставляющий целевую ротацию.</param>
        [Inject]
        public void Construct(IAxisRotationProvider axisRotationProvider)
        {
            this.axisRotationProvider = Utils.Extensions.AssignWithNullCheck(axisRotationProvider);
        }

        /// <summary>
        /// Обновление состояния вращения — прокидывает целевую ротацию в обработчик.
        /// </summary>
        public void Update()
        {
            OnRotation(axisRotationProvider.Rotation);
        }

        /// <summary>
        /// Вычисляет и применяет поворот к объекту.
        /// </summary>
        /// <param name="rotation">Целевая ротация.</param>
        protected abstract void OnRotation(Quaternion rotation);

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