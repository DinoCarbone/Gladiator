using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using Zenject;

namespace Core.Behaviors.States.Attack
{
    public class BaseAttack : BaseIncompatible
    {
        public BaseAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    /// <summary>
    /// Базовая реализация поведения атаки с обработкой входа/выхода.
    /// </summary>
    public class DefautAttack : BaseAttack, IEnterState, IExitState, IExitActivator
    {
        private IAttackProvider attackProvider;

        /// <summary>
        /// Создаёт экземпляр DefautAttack.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        public DefautAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        /// <summary>
        /// Инъекция провайдера атаки.
        /// </summary>
        /// <param name="attackProvider">Провайдер, определяющий состояние атаки.</param>
        [Inject]
        public void Construct(IAttackProvider attackProvider)
        {
            this.attackProvider = attackProvider;
        }

        /// <summary>
        /// Флаг, указывающий, можно ли выйти из состояния.
        /// </summary>
        public bool CanExit { get; private set; }

        /// <summary>
        /// Флаг, указывающий, можно ли войти в состояние (зависит от провайдера).
        /// </summary>
        public bool CanEnter => attackProvider.IsAttack;

        /// <summary>Событие выхода из состояния.</summary>
        public event Action OnExit;

        /// <summary>Событие входа в состояние.</summary>
        public event Action OnEnter;

        /// <summary>
        /// Выполняется при входе в состояние; сбрасывает флаг выхода и вызывает событие.
        /// </summary>
        public void Enter()
        {
            CanExit = false;
            OnEnter?.Invoke();
        }

        /// <summary>
        /// Выполняется при выходе из состояния; сбрасывает флаг выхода и вызывает событие.
        /// </summary>
        public void Exit()
        {
            CanExit = false;
            OnExit?.Invoke();
        }

        /// <summary>
        /// Активатор, позволяющий пометить состояние как готовое к выходу.
        /// </summary>
        public void ActivateExit()
        {
            CanExit = true;
        }
    }
}