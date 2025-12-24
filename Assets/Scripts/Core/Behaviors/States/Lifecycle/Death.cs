using System;
using System.Collections.Generic;
using Core.Behaviors.Lifecycle;
using Core.Providers;
using Core.Services.States;
using Utils;
using Zenject;

namespace Core.Behaviors.States.Lifecycle
{
    public class BaseDeath : BaseIncompatible
    {
        public BaseDeath(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    /// <summary>
    /// Реализация базового состояния смерти с обработкой уведомления о смерти.
    /// Управляет возможностью входа в состояние и регистрацией смерти в сервисе.
    /// </summary>
    public class DefautDeath : BaseDeath, IEnterState, IExitActivator, IDisposable
    {
        private readonly IKillableData killableData;
        private IDeathProvider deathProvider;
        private IDeathService deathService;

        /// <summary>
        /// Флаг, указывающий, можно ли войти в состояние смерти.
        /// </summary>
        public bool CanEnter { get; private set; }

        /// <summary>
        /// Событие, вызываемое при входе в состояние.
        /// </summary>
        public event Action OnEnter;

        /// <summary>
        /// Создаёт экземпляр DefaultDeath с данными о том, за что отвечает объект.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        /// <param name="killableData">Данные, описывающие убиваемый объект.</param>
        public DefautDeath(List<Type> incompatibleStates, IKillableData killableData) : base(incompatibleStates)
        {
            this.killableData = Extensions.AssignWithNullCheck(killableData);
        }

        /// <summary>
        /// Инъекция зависимостей: провайдер и сервис смерти. Подписывается на событие смерти.
        /// </summary>
        /// <param name="deathProvider">Провайдер, отдающий событие смерти.</param>
        /// <param name="deathService">Сервис регистрации смерти.</param>
        [Inject]
        private void Construct(IDeathProvider deathProvider, IDeathService deathService)
        {
            this.deathProvider = Extensions.AssignWithNullCheck(deathProvider);
            this.deathService = Extensions.AssignWithNullCheck(deathService);
            Subscribe();
        }

        /// <summary>
        /// Регистрирует смерть в сервисе (вызов со стороны активатора выхода).
        /// </summary>
        public void ActivateExit()
        {
            deathService.RegisterDeath(killableData);
        }

        /// <summary>
        /// Вызывает событие входа в состояние.
        /// </summary>
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        private void Subscribe()
        {
            deathProvider.OnDie += OnReciveDie;
        }

        private void Unsubscribe()
        {
            deathProvider.OnDie -= OnReciveDie;
        }

        private void OnReciveDie()
        {
            CanEnter = true;
        }

        /// <summary>
        /// Освобождает подписки и очищает ссылки на провайдеры.
        /// </summary>
        public void Dispose()
        {
            Unsubscribe();
            deathProvider = null;
        }
    }
}