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
    public class DefautDeath : BaseDeath, IEnterState, IExitActivator, IDisposable
    {
        private readonly IKillableData killableData;
        private IDeathProvider deathProvider;
        private IDeathService deathService;
        public bool CanEnter { get; private set; }
        public event Action OnEnter;

        public DefautDeath(List<Type> incompatibleStates, IKillableData killableData) : base(incompatibleStates)
        {
            this.killableData = Extensions.AssignWithNullCheck(killableData);
        }

        [Inject]
        private void Construct(IDeathProvider deathProvider, IDeathService deathService)
        {
            this.deathProvider = Extensions.AssignWithNullCheck(deathProvider);
            this.deathService = Extensions.AssignWithNullCheck(deathService);
            Subscribe();
        }
        public void ActivateExit()
        {
            deathService.RegisterDeath(killableData);
        }
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

        public void Dispose()
        {
            Unsubscribe();
            deathProvider = null;
        }
    }
}