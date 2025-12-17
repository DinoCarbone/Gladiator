using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Services.States
{
    public class StateMachine : StateMachineBase
    {
        private List<IState> statesToAdd = new List<IState>();
        private List<IState> statesToRemove = new List<IState>();
        private bool isProcessing = false;

        public StateMachine(
            List<IState> initialStateList,
            List<IState> idleStateList,
            Dictionary<Type, int> priorities = null)
            : base(initialStateList, idleStateList, priorities)
        {
        }

        public override void Update()
        {
            if (isProcessing) return;
            isProcessing = true;

            try
            {
                UpdateCurrentStates();
                ProcessExits();
                ProcessNewStates();
                EnsureActiveStates();
                ApplyPendingChanges();
            }
            finally
            {
                isProcessing = false;
                statesToAdd.Clear();
                statesToRemove.Clear();
            }
        }

        private void UpdateCurrentStates()
        {
            foreach (var state in currentStates)
            {
                if (updateHandlers.Count > 0)
                {
                    ExecuteHandler(state, updateHandlers);
                }
            }
        }

        private void ProcessExits()
        {
            for (int i = currentStates.Count - 1; i >= 0; i--)
            {
                var state = currentStates[i];

                if (canExitConditions.Count > 0 && CheckCondition(state, canExitConditions))
                {
                    if (exitHandlers.Count > 0)
                    {
                        ExecuteHandler(state, exitHandlers);
                    }
                    statesToRemove.Add(state);
                }
            }
        }

        private void ProcessNewStates()
        {
            foreach (var state in allStates)
            {
                // Пропускаем если уже активно
                if (currentStates.Contains(state) || statesToAdd.Contains(state))
                    continue;

                if (canEnterConditions.Count > 0 && !CheckCondition(state, canEnterConditions))
                    continue;

                if (CanAddState(state))
                {
                    statesToAdd.Add(state);

                    if (enterHandlers.Count > 0)
                    {
                        ExecuteHandler(state, enterHandlers);
                    }
                }
            }
        }

        private bool CanAddState(IState state)
        {
            var stateType = state.GetType();
            int statePriority = GetPriority(stateType);

            var stateIncompatible = GetIncompatibleTypes(state);

            foreach (var currentState in currentStates.Concat(statesToAdd))
            {
                if (currentState == state) continue;

                var currentType = currentState.GetType();

                if (stateIncompatible.Any(incompatible => incompatible.IsAssignableFrom(currentType)))
                {
                    int currentPriority = GetPriority(currentType);

                    if (currentPriority < statePriority)
                    {
                        return false;
                    }

                    if (!statesToRemove.Contains(currentState))
                    {
                        statesToRemove.Add(currentState);
                        if (exitHandlers.Count > 0)
                        {
                            ExecuteHandler(currentState, exitHandlers);
                        }
                    }
                }

                var currentIncompatible = GetIncompatibleTypes(currentState);
                if (currentIncompatible.Any(incompatible => incompatible.IsAssignableFrom(stateType)))
                {
                    int currentPriority = GetPriority(currentType);

                    if (currentPriority < statePriority)
                    {
                        Debug.LogWarning($"Cannot add {stateType.Name} - conflicts with higher priority {currentType.Name}");
                        return false;
                    }

                    if (!statesToRemove.Contains(currentState))
                    {
                        statesToRemove.Add(currentState);
                        if (exitHandlers.Count > 0)
                        {
                            ExecuteHandler(currentState, exitHandlers);
                        }
                    }
                }
            }

            return true;
        }

        private void EnsureActiveStates()
        {
            if (currentStates.Count == 0 && statesToAdd.Count == 0)
            {
                foreach (var idleState in idleStates)
                {
                    statesToAdd.Add(idleState);
                    if (enterHandlers.Count > 0)
                    {
                        ExecuteHandler(idleState, enterHandlers);
                    }
                }
            }
        }

        private void ApplyPendingChanges()
        {
            foreach (var state in statesToRemove)
            {
                currentStates.Remove(state);
            }

            foreach (var state in statesToAdd)
            {
                if (!currentStates.Contains(state))
                {
                    currentStates.Add(state);
                }
            }
        }
    }
}