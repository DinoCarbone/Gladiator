using System.Collections.Generic;
using Core.Services;
using Core.Services.DI;
using Core.Services.States;
using Data.ScriptableObjects.Behaviors;
using Data.ScriptableObjects.Providers; 
using Data.Serialization;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Entities
{
    public class EntityBase : MonoBehaviour
    {
        [SerializeField] private PriorityBehaviorsSO priorityBehaviorsSO;
        [SerializeField] private List<EntityDataPart> entityDatas = new List<EntityDataPart>();
        [SerializeField] private List<BaseProviderSO> providerSOs = new List<BaseProviderSO>();
        private List<IState> entityStates;
        private List<Providers.IProvider> providers;
        private StateMachine stateMachine;

        [Inject]
        private void Construct(IHybridInjectService hybridInjectService)
        {
            InitializeStates();
            InitializeProviders();
            InjectServices(hybridInjectService);
            print("Entity initialized");
        }
        private void InitializeStates()
        {
            entityStates = new List<IState>();
            List<IState> defaultStates = new List<IState>();
            foreach (EntityDataPart dataPart in entityDatas)
            {
                IState state = dataPart.behaviorSO.CreateConfigState(dataPart.contexts);
                entityStates.Add(state);

                if (dataPart.isDefaultState) defaultStates.Add(state);
            }
            Utils.Extensions.AssignWithNullCheck(priorityBehaviorsSO);
            stateMachine = new StateMachine(entityStates, defaultStates, priorityBehaviorsSO.GetPriorityTypes());
        }
        private void InitializeProviders()
        {
            providers = new List<Providers.IProvider>();
            foreach (BaseProviderSO providerSO in providerSOs)
            {
                Providers.IProvider provider = providerSO.CreateProvider();
                providers.Add(provider);
            }
        }
        private void InjectServices(IHybridInjectService hybridInjectService)
        {
            List<object> statesAsObjects = new List<object>(entityStates);
            statesAsObjects.AddRange(providers);

            hybridInjectService.InjectAll(statesAsObjects);
        }
        void Update()
        {
            stateMachine.Update();
        }
    }
}