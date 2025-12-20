using System;
using System.Collections.Generic;
using Core.Providers;
using Core.Services.States;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Attack
{
    public class BaseAttack : BaseIncompatible
    {
        public BaseAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    public class DefautAttack : BaseAttack, IEnterState, IExitState, IExitActivator
    {
        private IAttackProvider attackProvider;
        public DefautAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
        [Inject]
        public void Construct(IAttackProvider attackProvider)
        {
            this.attackProvider = attackProvider;
        }
        public bool CanExit { get; private set;}

        public bool CanEnter =>  attackProvider.IsAttack;

        public event Action OnExit;
        public event Action OnEnter;

        public void Enter()
        {
            Debug.Log("Enter attack");
            CanExit = false;
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            CanExit = false;
            OnExit?.Invoke();
        }

        public void ActivateExit()
        {
            CanExit = true;
        }
    }
}