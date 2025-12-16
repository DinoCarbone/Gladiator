using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.States;
using UnityEngine;

namespace Core.Services
{
    public class StateMachine
    {
        private readonly IReadOnlyList<IState> idleStates;
        private readonly List<IState> states = new List<IState>();
        private readonly HashSet<Type> cashed = new HashSet<Type>();
        private readonly IReadOnlyList<Type> priorityTypes;
        private List<IState> currentStates = new List<IState>();

        public StateMachine(List<IState> initialStates, IReadOnlyList<IState> idleStates, IReadOnlyList<Type> priorityTypes)
        {
            this.priorityTypes = Utils.Extensions.AssignWithNullCheck(priorityTypes);
            states = Utils.Extensions.AssignWithNullCheck(initialStates);
            this.idleStates = Utils.Extensions.AssignWithNullCheck(idleStates);
            currentStates = this.idleStates.ToList();
            if (states.Count == 0 || currentStates.Count == 0)
            {
                throw new Exception("StateMachine must have at least one state and one current state.");
            }
            CheckCorrectIdleState();
        }

        public virtual void Update()
        {
            foreach (IState state in currentStates)
            {
                if (state is IUpdateState updateState)
                {
                    updateState.Update();
                }
                if (state is IExitable exitableState)
                {
                    if (exitableState.CanExit)
                    {
                        if (exitableState is IExitState exitState)
                        {
                            exitState.Exit();
                        }
                        
                        currentStates.Remove(state);
                    }
                }
            }

            ReplaceWithPriorityStates();

            if (currentStates.Count == 0)
            {
                currentStates.AddRange(idleStates);
            }
            else
            {
                AddPartDefaultStates();
            }
        }
        private void ReplaceWithPriorityStates()
        {
            foreach (IState state in states)
            {
                if (state is IEnterable enterableState && enterableState.CanEnter)
                {
                    if (state is IIncompatibleStates incompatible)
                    {
                        // Собираем ВСЕ конфликтующие состояния
                        var conflictingStates = new List<IState>();

                        foreach (Type type in incompatible.IncompatibleStates)
                        {
                            IState toRemove = currentStates.FirstOrDefault(s => type.IsAssignableFrom(s.GetType()));
                            if (toRemove != null)
                            {
                                conflictingStates.Add(toRemove);
                            }
                        }

                        // Если есть конфликты, проверяем приоритеты ВСЕХ
                        if (conflictingStates.Count > 0)
                        {
                            int stateIndex = Utils.Extensions.GetTypeIndexInPriorityList(state.GetType(), priorityTypes);

                            // Проверяем, есть ли среди конфликтующих состояний те,
                            // которые имеют более высокий приоритет (меньший индекс)
                            bool hasHigherPriorityConflict = false;

                            foreach (var toRemove in conflictingStates)
                            {
                                int toRemoveIndex = Utils.Extensions.GetTypeIndexInPriorityList(toRemove.GetType(), priorityTypes);
                                if (toRemoveIndex < stateIndex) // Более высокий приоритет
                                {
                                    hasHigherPriorityConflict = true;
                                    break;
                                }
                            }

                            // Если есть хоть одно состояние с более высоким приоритетом
                            // НЕ добавляем новое состояние
                            if (hasHigherPriorityConflict)
                            {
                                Debug.LogWarning($"Cannot add {state.GetType().Name} - " +
                                               $"conflicts with higher priority state(s)");
                                continue; // Пропускаем это состояние
                            }

                            // Если ВСЕ конфликтующие состояния имеют более низкий приоритет
                            // Удаляем их ВСЕХ
                            foreach (var toRemove in conflictingStates)
                            {
                                if (toRemove is IExitState exitState)
                                {
                                    exitState.Exit();
                                }
                                currentStates.Remove(toRemove);
                            }
                        }
                    }

                    // Добавляем новое состояние
                    if (state is IEnterState enterState)
                    {
                        enterState.Enter();
                    }
                    currentStates.Add(state);
                }
            }
        }
        private void AddPartDefaultStates()
        {
            cashed.Clear();
            foreach (IState state in currentStates)
            {
                if (state is IIncompatibleStates incompatible)
                {
                    foreach (Type type in incompatible.IncompatibleStates)
                    {
                        cashed.Add(type);
                    }
                }
            }
            foreach (IState state in idleStates)
            {
                if(currentStates.Contains(state))
                    continue;
                foreach(Type type in cashed)
                {
                    if(type.IsAssignableFrom(state.GetType()))
                    continue;
                }
                if (!cashed.Contains(state.GetType()))
                {
                    currentStates.Add(state);
                }
            }
        }
        private void CheckCorrectIdleState()
        {
            Utils.Extensions.ValidateStatesCompatibility(currentStates, idleStates);
        }
    }
}