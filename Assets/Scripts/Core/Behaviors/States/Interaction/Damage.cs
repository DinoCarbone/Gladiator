using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using Zenject;
using Utils;

namespace Core.Behaviors.States.Interaction
{
    public class BaseDamage : BaseIncompatible
    {
        public BaseDamage(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public class EmptyDamage : BaseDamage, IEnterState, IExitState, IExitActivator, IDisposable
    {
        private IDamageProvider damageProvider;

        /// <summary>Создаёт пустое поведение урона с набором несовместимых состояний.</summary>
        public EmptyDamage(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        /// <summary>Флаг — можно ли войти в состояние.</summary>
        public bool CanEnter { get; private set; }

        /// <summary>Флаг — можно ли выйти из состояния.</summary>
        public bool CanExit { get; private set; }

        /// <summary>Событие входа в состояние.</summary>
        public event Action OnEnter;

        /// <summary>Событие выхода из состояния.</summary>
        public event Action OnExit;

        /// <summary>Внедрение зависимостей через Zenject: получает провайдера урона и подписывается.</summary>
        [Inject]
        private void Construct(IDamageProvider damageProvider)
        {
            this.damageProvider = Extensions.AssignWithNullCheck(damageProvider);
            Subscribe();
        }

        /// <summary>Выполняется при входе в состояние.</summary>
        public void Enter()
        {
            CanEnter = false;
            OnEnter?.Invoke();
        }

        /// <summary>Выполняется при выходе из состояния.</summary>
        public void Exit()
        {
            CanExit = false;
            OnExit?.Invoke();
        }

        private void Subscribe()
        {
            damageProvider.OnTakeDamage += OnReceiveDamage;
        }

        private void Unsubscribe()
        {
            damageProvider.OnTakeDamage -= OnReceiveDamage;
        }

        /// <summary>Убирает подписки и освобождает ссылки.</summary>
        public void Dispose()
        {
            Unsubscribe();
            damageProvider = null;
        }

        private void OnReceiveDamage(int obj)
        {
            CanEnter = true;
        }

        /// <summary>Активирует возможность выхода из состояния.</summary>
        public void ActivateExit()
        {
            CanExit = true;
        }
    }
}