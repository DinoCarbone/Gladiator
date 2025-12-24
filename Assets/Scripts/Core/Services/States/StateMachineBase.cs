using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.States
{
    /// <summary>
    /// Базовый класс для машин состояний. Обеспечивает хранение и кэширование метаданных состояний,
    /// регистрацию стандартных обработчиков входа/выхода/обновления и логику приоритетов.
    /// </summary>
    public abstract class StateMachineBase
    {
        protected List<StateWithType> currentStates = new List<StateWithType>();
        protected List<StateWithType> allStates = new List<StateWithType>();
        protected List<StateWithType> idleStates = new List<StateWithType>();

        protected Dictionary<Type, StateCachedData> stateCachedData = new Dictionary<Type, StateCachedData>();

        protected Dictionary<Type, StateActionDelegate> enterHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateActionDelegate> exitHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateActionDelegate> updateHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateConditionDelegate> canEnterConditions = new Dictionary<Type, StateConditionDelegate>();
        protected Dictionary<Type, StateConditionDelegate> canExitConditions = new Dictionary<Type, StateConditionDelegate>();
        protected Dictionary<Type, StateIncompatibleDelegate> incompatibleGetters = new Dictionary<Type, StateIncompatibleDelegate>();

        protected Dictionary<Type, int> statePriorities = new Dictionary<Type, int>();

        protected StateMachineBase(List<IState> initialStateList, List<IState> idleStateList, Dictionary<Type, int> priorities = null)
        {
            statePriorities = priorities ?? new Dictionary<Type, int>();

            allStates = ConvertToStateWithTypeList(initialStateList ?? throw new ArgumentNullException(nameof(initialStateList)));
            idleStates = ConvertToStateWithTypeList(idleStateList ?? throw new ArgumentNullException(nameof(idleStateList)));

            CacheStateData();
            RegisterStandardHandlers();
        }

        private List<StateWithType> ConvertToStateWithTypeList(List<IState> states)
        {
            var result = new List<StateWithType>();
            foreach (var state in states)
            {
                result.Add(new StateWithType(state));
            }
            return result;
        }

        protected virtual void RegisterStandardHandlers()
        {
            RegisterActionHandler<IEnterState>(new StateActionDelegate((state) => ((IEnterState)state).Enter()), enterHandlers);
            RegisterActionHandler<IExitState>(new StateActionDelegate((state) => ((IExitState)state).Exit()), exitHandlers);
            RegisterActionHandler<IUpdateState>(new StateActionDelegate((state) => ((IUpdateState)state).Update()), updateHandlers);

            RegisterConditionHandler<IEnterable>(new StateConditionDelegate((state) => ((IEnterable)state).CanEnter), canEnterConditions);
            RegisterConditionHandler<IExitable>(new StateConditionDelegate((state) => ((IExitable)state).CanExit), canExitConditions);

            RegisterIncompatibleHandler<IIncompatibleStates>(new StateIncompatibleDelegate((state) => ((IIncompatibleStates)state).IncompatibleStates), incompatibleGetters);
        }

        protected void RegisterActionHandler<T>(StateActionDelegate handler, Dictionary<Type, StateActionDelegate> handlers) where T : IState
        {
            handlers[typeof(T)] = handler;
        }

        protected void RegisterConditionHandler<T>(StateConditionDelegate condition, Dictionary<Type, StateConditionDelegate> conditions) where T : IState
        {
            conditions[typeof(T)] = condition;
        }

        protected void RegisterIncompatibleHandler<T>(StateIncompatibleDelegate getter, Dictionary<Type, StateIncompatibleDelegate> getters) where T : IState
        {
            getters[typeof(T)] = getter;
        }

        private void CacheStateData()
        {
            CacheStateDataForList(allStates);
            CacheStateDataForList(idleStates);
        }

        private void CacheStateDataForList(List<StateWithType> states)
        {
            foreach (var stateWithType in states)
            {
                var type = stateWithType.Type;
                if (stateCachedData.ContainsKey(type))
                    continue;

                var data = new StateCachedData();
                var allTypes = new List<Type>();
                
                var currentType = type;
                while (currentType != null && currentType != typeof(object))
                {
                    allTypes.Add(currentType);
                    allTypes.AddRange(currentType.GetInterfaces());
                    currentType = currentType.BaseType;
                }

                data.AllAssignableTypes = new HashSet<Type>(allTypes);

                data.Priority = GetPriorityUncached(type);

                stateCachedData[type] = data;
            }
        }

        private int GetPriorityUncached(Type type)
        {
            foreach (var kvp in statePriorities)
            {
                if (kvp.Key.IsAssignableFrom(type))
                {
                    return kvp.Value;
                }
            }

            Debug.LogWarning($"Приоритет не найден для типа: {type.Name}");
            return int.MaxValue;
        }

        public abstract void Update();

        protected void ExecuteHandler(StateWithType state, Dictionary<Type, StateActionDelegate> handlers)
        {
            var cachedData = stateCachedData[state.Type];

            foreach (var handlerType in handlers.Keys)
            {
                if (cachedData.AllAssignableTypes.Contains(handlerType))
                {
                    handlers[handlerType]?.Invoke(state.State);
                }
            }
        }

        protected bool CheckCondition(StateWithType state, Dictionary<Type, StateConditionDelegate> conditions)
        {
            var cachedData = stateCachedData[state.Type];

            foreach (var conditionType in conditions.Keys)
            {
                if (cachedData.AllAssignableTypes.Contains(conditionType))
                {
                    return conditions[conditionType]?.Invoke(state.State) ?? false;
                }
            }
            return false;
        }

        protected IReadOnlyList<Type> GetIncompatibleTypes(StateWithType state)
        {
            var cachedData = stateCachedData[state.Type];

            foreach (var getterType in incompatibleGetters.Keys)
            {
                if (cachedData.AllAssignableTypes.Contains(getterType))
                {
                    return incompatibleGetters[getterType]?.Invoke(state.State) ?? Array.Empty<Type>();
                }
            }
            return Array.Empty<Type>();
        }

        protected int GetPriority(StateWithType state)
        {
            return stateCachedData.TryGetValue(state.Type, out var data) ? data.Priority : int.MaxValue;
        }

        protected int GetPriority(Type type)
        {
            return stateCachedData.TryGetValue(type, out var data) ? data.Priority : int.MaxValue;
        }

        protected class StateWithType
        {
            public IState State { get; }
            public Type Type { get; }

            public StateWithType(IState state)
            {
                State = state;
                Type = state.GetType(); 
            }
        }
        
        protected class StateCachedData
        {
            public HashSet<Type> AllAssignableTypes;
            public int Priority;
        }
    }
}