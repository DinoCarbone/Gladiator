using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.States
{
    public abstract class StateMachineBase
    {
        protected List<IState> currentStates = new List<IState>();
        protected List<IState> allStates = new List<IState>();
        protected List<IState> idleStates = new List<IState>();

        protected Dictionary<Type, StateActionDelegate> enterHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateActionDelegate> exitHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateActionDelegate> updateHandlers = new Dictionary<Type, StateActionDelegate>();
        protected Dictionary<Type, StateConditionDelegate> canEnterConditions = new Dictionary<Type, StateConditionDelegate>();
        protected Dictionary<Type, StateConditionDelegate> canExitConditions = new Dictionary<Type, StateConditionDelegate>();
        protected Dictionary<Type, StateIncompatibleDelegate> incompatibleGetters = new Dictionary<Type, StateIncompatibleDelegate>();

        protected Dictionary<Type, int> statePriorities = new Dictionary<Type, int>();

        protected StateMachineBase(List<IState> initialStateList, List<IState> idleStateList, Dictionary<Type, int> priorities = null)
        {
            allStates = initialStateList ?? throw new ArgumentNullException(nameof(initialStateList));
            idleStates = idleStateList ?? throw new ArgumentNullException(nameof(idleStateList));
            statePriorities = priorities ?? new Dictionary<Type, int>();
            // if (allStates.Count == 0 || idleStates.Count == 0)
            //     throw new ArgumentException("Must have at least one state and one idle state");

            RegisterStandardHandlers();
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

        public abstract void Update();

        protected void ExecuteHandler(IState state, Dictionary<Type, StateActionDelegate> handlers)
        {
            var stateType = state.GetType();
            foreach (var handlerType in handlers.Keys)
            {
                if (handlerType.IsAssignableFrom(stateType))
                {
                    handlers[handlerType]?.Invoke(state);
                }
            }
        }

        protected bool CheckCondition(IState state, Dictionary<Type, StateConditionDelegate> conditions)
        {
            var stateType = state.GetType();
            foreach (var conditionType in conditions.Keys)
            {
                if (conditionType.IsAssignableFrom(stateType))
                {
                    return conditions[conditionType]?.Invoke(state) ?? false;
                }
            }
            return false;
        }

        protected IReadOnlyList<Type> GetIncompatibleTypes(IState state)
        {
            var stateType = state.GetType();
            foreach (var getterType in incompatibleGetters.Keys)
            {
                if (getterType.IsAssignableFrom(stateType))
                {
                    return incompatibleGetters[getterType]?.Invoke(state) ?? Array.Empty<Type>();
                }
            }
            return Array.Empty<Type>();
        }

        protected int GetPriority(Type type)
        {
            foreach (var kvp in statePriorities)
            {
                if (kvp.Key.IsAssignableFrom(type))
                {
                    return kvp.Value;
                }
            }

            // 3. Не нашли
            Debug.LogWarning($"Приоритет не найден для типа: {type.Name}");
            return int.MaxValue;
        }
    }
}