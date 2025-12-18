using System.Collections.Generic;
using Core.Services.DI;
using Core.Services.States;
using Data.ScriptableObjects.Behaviors;
using Data.Serialization;
using UnityEngine;
using Zenject;
using Utils;
using Core.Providers;
using System;

namespace Core.Behaviors.Entities
{
    public class EntityBase : MonoBehaviour
    {
        [SerializeField] private PriorityBehaviorsSO priorityBehaviorsSO;
        [SerializeField] private List<EntityDataPart> entityDatas = new List<EntityDataPart>();
        [SerializeField] private List<ProviderDataPart> providersSO = new List<ProviderDataPart>();
        private List<IState> entityStates;
        private List<Providers.IProvider> providers;
        private StateMachine stateMachine;

        [Inject]
        private void Construct(IHybridInjectService hybridInjectService)
        {
            InitializeStates();
            InitializeProviders();
            InjectServices(hybridInjectService);
        }
        void OnDestroy()
        {
            if (entityStates != null)
            {
                foreach (IState state in entityStates)
                {
                    if (state is IDisposable disposable) disposable.Dispose();
                }
            }
            if (providers != null)
            {
                foreach (Providers.IProvider provider in providers)
                {
                    if (provider is IDisposable disposable) disposable.Dispose();
                }
            }
            stateMachine = null;
            entityStates = null;
            providers = null;
        }
        private void InitializeStates()
        {
            entityStates = new List<IState>();
            List<IState> defaultStates = new List<IState>();
            foreach (EntityDataPart dataPart in entityDatas)
            {
                IState state = dataPart.behaviorSO.CreateConfigState(dataPart.contexts);

                if(!Extensions.ContainsType(entityStates, state)) entityStates.Add(state);
                else Debug.LogError("Behavior type already exists");

                if (dataPart.isDefaultState) defaultStates.Add(state);
            }
            Extensions.AssignWithNullCheck(priorityBehaviorsSO);
            stateMachine = new StateMachine(entityStates, defaultStates, priorityBehaviorsSO.GetPriorityTypes());

        }
        private void InitializeProviders()
        {
            providers = new List<Providers.IProvider>();
            foreach (ProviderDataPart providerData in providersSO)
            {
                Providers.IProvider provider = providerData.providerSO.CreateProvider(providerData.contexts);

                if(!Extensions.ContainsType(providers, provider)) providers.Add(provider);
                else Debug.LogError("Provider type already exists");
            }
        }
        private void InjectServices(IHybridInjectService hybridInjectService)
        {
            List<object> statesAsObjects = new List<object>(entityStates);
            statesAsObjects.AddRange(providers);

            StateListData stateListData = new StateListData(entityStates);
            statesAsObjects.Add(stateListData);
            
            hybridInjectService.InjectAll(statesAsObjects);
        }
        void Update()
        {
            stateMachine.Update();
        }
    }
}