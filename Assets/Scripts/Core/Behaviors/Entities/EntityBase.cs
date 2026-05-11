using System.Collections.Generic;
using Core.Services.DI;
using Core.Services.States;
using Data.Dto;
using UnityEngine;
using Zenject;
using Utils;
using System;
using Data.ScriptableObjects.States;
using System.Linq;

namespace Core.Behaviors.Entities
{
    /// <summary>
    /// Базовый компонент сущности: собирает состояния и провайдеры из ScriptableObject-частей,
    /// создаёт StateMachine и выполняет инъекцию зависимостей через <see cref="IHybridInjectService"/>.
    /// </summary>
    public class EntityBase : MonoBehaviour
    {
        [SerializeField, Tooltip("Reference to the priority behaviors configuration.")]
        private PriorityBehaviorsSO priorityBehaviorsSO;

        [SerializeField, Tooltip("List of entity data parts for this entity.")]
        private List<EntityDataPart> entityDatas = new List<EntityDataPart>();

        [SerializeField, Tooltip("List of provider ScriptableObjects to attach to this entity.")]
        private List<ProviderDataPart> providersSO = new List<ProviderDataPart>();

        private List<IState> entityStates;
        private List<Providers.IProvider> providers;
        private StateMachine stateMachine;

        /// <summary>
        /// Инжектирует сервисы и выполняет инициализацию состояний и провайдеров.
        /// </summary>
        /// <param name="hybridInjectService">Сервис, отвечающий за инъекции зависимостей.</param>
        [Inject]
        private void Construct(IHybridInjectService hybridInjectService)
        {
            InitializeStates();
            InitializeProviders();
            InjectServices(hybridInjectService);
        }

        /// <summary>Освобождает созданные состояния и провайдеры при уничтожении объекта.</summary>
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

        /// <summary>
        /// Создаёт список состояний на основе настроек <see cref="entityDatas"/> и инициализирует StateMachine.
        /// </summary>
        private void InitializeStates()
        {
            entityStates = new List<IState>();
            List<IState> defaultStates = new List<IState>();

            foreach (EntityDataPart dataPart in entityDatas)
            {
                object[] deps = null;
                if (dataPart.contexts != null && dataPart.contexts.Count > 0)
                {
                    deps = dataPart.contexts.Cast<object>().ToArray();
                }

                IState state = dataPart.behaviorSO.CreateConfigState(deps ?? Array.Empty<object>());

                if (!Extensions.ContainsType(entityStates, state)) entityStates.Add(state);
                else Debug.LogError("Behavior type already exists");

                if (dataPart.isDefaultState) defaultStates.Add(state);
            }

            Extensions.AssignWithNullCheck(priorityBehaviorsSO);
            stateMachine = new StateMachine(entityStates, defaultStates, priorityBehaviorsSO.GetPriorityTypes());
        }

        /// <summary>
        /// Создаёт провайдеры, описанные в <see cref="providersSO"/>.
        /// </summary>
        private void InitializeProviders()
        {
            providers = new List<Providers.IProvider>();
            foreach (ProviderDataPart providerData in providersSO)
            {
                // Собираем зависимости в том порядке, как объявлено в SO
                object[] deps = null;
                if (providerData.contexts != null && providerData.contexts.Count > 0)
                {
                    deps = providerData.contexts.Cast<object>().ToArray();
                }

                Providers.IProvider provider = providerData.providerSO.CreateProvider(deps ?? Array.Empty<object>());

                if (!Extensions.ContainsType(providers, provider)) providers.Add(provider);
                else Debug.LogError("Provider type already exists");
            }
        }

        /// <summary>
        /// Подготавливает объекты для инъекции и передаёт их в <see cref="IHybridInjectService.InjectAll"/>.
        /// </summary>
        /// <param name="hybridInjectService">Сервис инъекции зависимостей.</param>
        private void InjectServices(IHybridInjectService hybridInjectService)
        {
            List<object> statesAsObjects = new List<object>(entityStates);
            statesAsObjects.AddRange(providers);

            StateListData stateListData = new StateListData(entityStates);
            statesAsObjects.Add(stateListData);

            AllEntityData allEntityData = new AllEntityData(statesAsObjects);
            statesAsObjects.Add(allEntityData);

            hybridInjectService.InjectAll(statesAsObjects);
        }

        /// <summary>Вызван каждый кадр — проксирует обновление в StateMachine.</summary>
        void Update()
        {
            stateMachine.Update();
        }
    }
}