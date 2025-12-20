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
        private HashSet<IState> exitingStates = new HashSet<IState>();

        // Чтобы не создавать новые списки и не засорять GC
        private List<IState> cachedTempList = new List<IState>();

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
            // Используем кешированный список для безопасной итерации
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
            // Итерируем в обратном порядке
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
            // Собираем кандидатов в кешированный список
            cachedTempList.Clear();

            foreach (var state in allStates)
            {
                // Быстрая проверка через Contains
                if (currentStates.Contains(state) || exitingStates.Contains(state) || statesToAdd.Contains(state))
                    continue;

                if (canEnterConditions.Count > 0 && !CheckCondition(state, canEnterConditions))
                    continue;

                cachedTempList.Add(state);
            }

            // Сортируем по приоритету (высший по списку последний)
            cachedTempList.Sort((b, a) => GetPriority(b.GetType()).CompareTo(GetPriority(a.GetType())));

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

        private bool CanAddState(IState newState)
        {
            var newStateType = newState.GetType();
            int newStatePriority = GetPriority(newStateType);
            var newStateIncompatible = GetIncompatibleTypes(newState);

            // Проверяем текущие состояния (которые не будут удалены)
            for (int i = 0; i < currentStates.Count; i++)
            {
                var activeState = currentStates[i];

                // Пропускаем состояния, которые будут удалены
                if (statesToRemove.Contains(activeState))
                    continue;

                if (CheckStateConflict(activeState, newState, newStateType, newStatePriority, newStateIncompatible))
                    return false;
            }

            for (int i = 0; i < statesToAdd.Count; i++)
            {
                var pendingState = statesToAdd[i];
                if (pendingState == newState)
                    continue;

                if (CheckStateConflict(pendingState, newState, newStateType, newStatePriority, newStateIncompatible))
                    return false;
            }
            // Проверяем состояния, которые будут добавлены (кроме самого newState)

            return true;
        }

        private bool CheckStateConflict(IState existingState, IState newState, Type newStateType, int newStatePriority, IReadOnlyList<Type> newStateIncompatible)
        {
            var existingType = existingState.GetType();

            // 1. Проверяем, несовместимо ли новое состояние с существующим
            for (int i = 0; i < newStateIncompatible.Count; i++)
            {
                if (newStateIncompatible[i].IsAssignableFrom(existingType))
                {
                    return HandleConflict(existingState, existingType, newStateType, newStatePriority);
                }
            }

            // 2. Проверяем, несовместимо ли существующее состояние с новым
            var existingIncompatible = GetIncompatibleTypes(existingState);
            for (int i = 0; i < existingIncompatible.Count; i++)
            {
                if (existingIncompatible[i].IsAssignableFrom(newStateType))
                {
                    return HandleConflict(existingState, existingType, newStateType, newStatePriority);
                }
            }

            return false; // Конфликта нет
        }

        private bool HandleConflict(IState conflictingState, Type conflictingType, Type newStateType, int newStatePriority)
        {
            int conflictingPriority = GetPriority(conflictingType);

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
            // Используем кешированный список для расчета будущих состояний
            cachedTempList.Clear();
            cachedTempList.AddRange(currentStates);

            // Удаляем состояния, которые будут удалены
            for (int i = cachedTempList.Count - 1; i >= 0; i--)
            {
                if (statesToRemove.Contains(cachedTempList[i]))
                {
                    cachedTempList.RemoveAt(i);
                }
            }

            // Добавляем состояния, которые будут добавлены
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
            // Удаляем состояния (итерируем в обратном порядке для эффективности)
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
                var type = state.GetType();
                Debug.Log($"  - {type.Name} (Priority: {GetPriority(type)})");
            }
        }
    }
}