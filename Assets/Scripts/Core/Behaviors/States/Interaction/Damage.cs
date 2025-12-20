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
    public class EmptyDamage : BaseDamage, IEnterState, IExitState,IExitActivator, IDisposable
    {
        private IDamageProvider damageProvider;
        public EmptyDamage(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        public bool CanEnter { get; private set; }

        public bool CanExit { get; private set; }

        public event Action OnEnter;
        public event Action OnExit;

        [Inject]
        private void Construct(IDamageProvider damageProvider)
        {
            this.damageProvider = Extensions.AssignWithNullCheck(damageProvider);
            Subscribe();
        }

        public void Enter()
        {
            CanEnter = false;
            OnEnter?.Invoke();
        }

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
        public void Dispose()
        {
            Unsubscribe();
            damageProvider = null;
        }
        private void OnReceiveDamage(int obj)
        {
            CanEnter = true;
        }
        public void ActivateExit()
        {
            CanExit = true;
        }
    }
}