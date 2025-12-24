using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.States
{
    /// <summary>
    /// Реализация машины состояний с приоритетами, отложенными изменениями и кешированием несовместимостей.
    /// </summary>
    public class StateMachine : StateMachineBase
    {
        private List<StateWithType> statesToAdd = new List<StateWithType>();
        private List<StateWithType> statesToRemove = new List<StateWithType>();
        private bool isProcessing = false;
        private HashSet<StateWithType> exitingStates = new HashSet<StateWithType>();

        // Чтобы не создавать новые списки и не засорять GC
        private List<StateWithType> cachedTempList = new List<StateWithType>();
        private Dictionary<Type, IReadOnlyList<Type>> incompatibleTypesCache = new Dictionary<Type, IReadOnlyList<Type>>();

        /// <summary>
        /// Создаёт машину состояний.
        /// </summary>
        /// <param name="initialStateList">Список начальных состояний.</param>
        /// <param name="idleStateList">Список idle-состояний.</param>
        /// <param name="priorities">Словарь приоритетов по типу состояния.</param>
        public StateMachine(
            List<IState> initialStateList,
            List<IState> idleStateList,
            Dictionary<Type, int> priorities = null)
            : base(initialStateList, idleStateList, priorities)
        {
        }

        /// <summary>
        /// Основной цикл машины состояний: обрабатывает выходы, обновляет активные состояния,
        /// проверяет входы и применяет отложенные изменения.
        /// </summary>
        public override void Update()
        {
            if (isProcessing) return;
            isProcessing = true;

            try
            {
                ProcessExits();
                UpdateCurrentStates();
                ProcessNewStates();
                EnsureActiveStates();
                ApplyPendingChanges();
            }
            finally
            {
                isProcessing = false;
                statesToAdd.Clear();
                statesToRemove.Clear();
                exitingStates.Clear();
            }
        }

        private void UpdateCurrentStates()
        {
            cachedTempList.Clear();
            cachedTempList.AddRange(currentStates);

            foreach (var state in cachedTempList)
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

                if (exitingStates.Contains(state))
                    continue;

                if (canExitConditions.Count > 0 && CheckCondition(state, canExitConditions))
                {
                    exitingStates.Add(state);
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
            cachedTempList.Clear();

            foreach (var state in allStates)
            {
                if (currentStates.Contains(state) || exitingStates.Contains(state) || statesToAdd.Contains(state))
                    continue;

                if (canEnterConditions.Count > 0 && !CheckCondition(state, canEnterConditions))
                    continue;

                cachedTempList.Add(state);
            }

            // Сортируем по приоритету (высший по списку первый)
            cachedTempList.Sort((a, b) => GetPriority(a).CompareTo(GetPriority(b)));

            // Обрабатываем кандидатов
            for (int i = 0; i < cachedTempList.Count; i++)
            {
                var state = cachedTempList[i];
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

        private bool CanAddState(StateWithType newState)
        {
            int newStatePriority = GetPriority(newState);
            var newStateIncompatible = GetCachedIncompatibleTypes(newState);

            // Проверяем текущие состояния (которые не будут удалены)
            for (int i = 0; i < currentStates.Count; i++)
            {
                var activeState = currentStates[i];

                // Пропускаем состояния, которые будут удалены
                if (statesToRemove.Contains(activeState))
                    continue;

                if (CheckStateConflict(activeState, newState, newStatePriority, newStateIncompatible))
                    return false;
            }

            for (int i = 0; i < statesToAdd.Count; i++)
            {
                var pendingState = statesToAdd[i];
                if (pendingState == newState)
                    continue;

                if (CheckStateConflict(pendingState, newState, newStatePriority, newStateIncompatible))
                    return false;
            }

            return true;
        }

        private IReadOnlyList<Type> GetCachedIncompatibleTypes(StateWithType state)
        {
            var stateType = state.Type;
            if (!incompatibleTypesCache.TryGetValue(stateType, out var incompatibleTypes))
            {
                incompatibleTypes = GetIncompatibleTypes(state);
                incompatibleTypesCache[stateType] = incompatibleTypes;
            }
            return incompatibleTypes;
        }

        private bool CheckStateConflict(StateWithType existingState, StateWithType newState, int newStatePriority, IReadOnlyList<Type> newStateIncompatible)
        {
            var existingCachedData = stateCachedData[existingState.Type];

            // 1. Проверяем, несовместимо ли новое состояние с существующим
            for (int i = 0; i < newStateIncompatible.Count; i++)
            {
                if (existingCachedData.AllAssignableTypes.Contains(newStateIncompatible[i]))
                {
                    return HandleConflict(existingState, newState, newStatePriority);
                }
            }

            // 2. Проверяем, несовместимо ли существующее состояние с новым
            var existingIncompatible = GetCachedIncompatibleTypes(existingState);
            var newCachedData = stateCachedData[newState.Type];
            
            for (int i = 0; i < existingIncompatible.Count; i++)
            {
                if (newCachedData.AllAssignableTypes.Contains(existingIncompatible[i]))
                {
                    return HandleConflict(existingState, newState, newStatePriority);
                }
            }

            return false; // Конфликта нет
        }

        private bool HandleConflict(StateWithType conflictingState, StateWithType newState, int newStatePriority)
        {
            int conflictingPriority = GetPriority(conflictingState);

            // Существующее состояние имеет ВЫСШИЙ приоритет - новый не может быть добавлен
            if (conflictingPriority < newStatePriority)
            {
                return true; // Есть конфликт, который нельзя разрешить
            }

            // Существующее состояние имеет НИЖНИЙ или РАВНЫЙ приоритет - вытесняем его
            if (!exitingStates.Contains(conflictingState) && !statesToRemove.Contains(conflictingState))
            {
                exitingStates.Add(conflictingState);
                statesToRemove.Add(conflictingState);
                if (exitHandlers.Count > 0)
                {
                    ExecuteHandler(conflictingState, exitHandlers);
                }
            }

            return false; // Конфликт разрешен (состояние будет вытеснено)
        }

        private void EnsureActiveStates()
        {
            cachedTempList.Clear();
            cachedTempList.AddRange(currentStates);

            for (int i = cachedTempList.Count - 1; i >= 0; i--)
            {
                if (statesToRemove.Contains(cachedTempList[i]))
                {
                    cachedTempList.RemoveAt(i);
                }
            }

            for (int i = 0; i < statesToAdd.Count; i++)
            {
                var state = statesToAdd[i];
                if (!cachedTempList.Contains(state))
                {
                    cachedTempList.Add(state);
                }
            }

            // Если не останется активных состояний - добавляем idle
            if (cachedTempList.Count == 0)
            {
                for (int i = 0; i < idleStates.Count; i++)
                {
                    var idleState = idleStates[i];
                    if (!statesToAdd.Contains(idleState))
                    {
                        statesToAdd.Add(idleState);
                        if (enterHandlers.Count > 0)
                        {
                            ExecuteHandler(idleState, enterHandlers);
                        }
                    }
                }
            }
        }

        private void ApplyPendingChanges()
        {
            for (int i = statesToRemove.Count - 1; i >= 0; i--)
            {
                currentStates.Remove(statesToRemove[i]);
            }

            for (int i = 0; i < statesToAdd.Count; i++)
            {
                var state = statesToAdd[i];
                if (!currentStates.Contains(state))
                {
                    currentStates.Add(state);
                }
            }
        }

        public void DebugLogCurrentState()
        {
            Debug.Log($"Active states ({currentStates.Count}):");
            for (int i = 0; i < currentStates.Count; i++)
            {
                var state = currentStates[i];
                Debug.Log($"  - {state.Type.Name} (Priority: {GetPriority(state)})");
            }
        }
    }
}