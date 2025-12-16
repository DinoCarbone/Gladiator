using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.States;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {

        /// <summary>
        /// Assigns a value with null check. Throws ArgumentNullException if value is null.
        /// </summary>
        public static T AssignWithNullCheck<T>(T value) where T : class
        {
            if (value == null)
            {
                string errorMessage = $"Value of type {typeof(T).Name} cannot be null";

                Debug.LogError(errorMessage);
            }

            return value;
        }

        /// <exception cref="ArgumentException">Thrown when incompatible states are detected</exception>
        public static bool ValidateStatesCompatibility<T>(
       IReadOnlyList<T> statesA,
       IReadOnlyList<T> statesB,
       UnityEngine.Object context = null) where T : class, IState
        {
            if (statesA == null) throw new ArgumentNullException(nameof(statesA));
            if (statesB == null) throw new ArgumentNullException(nameof(statesB));

            if (statesA.Count == 0 || statesB.Count == 0)
            {
                return true;
            }

            bool hasConflicts = false;
            var conflictMessages = new List<string>();

            var stateBTypes = new HashSet<Type>(statesB.Select(s => s.GetType()));

            // Проверяем каждое состояние из списка A
            foreach (var stateA in statesA)
            {
                if (stateA is IIncompatibleStates incompatibleA)
                {
                    var conflictsWithB = stateBTypes
                        .Where(typeB => incompatibleA.IncompatibleStates.Contains(typeB))
                        .ToList();

                    if (conflictsWithB.Count > 0)
                    {
                        hasConflicts = true;
                        foreach (var conflictingType in conflictsWithB)
                        {
                            string conflictMsg =
                                $"Состояние {stateA.GetType().Name} несовместимо с состоянием {conflictingType.Name}";
                            conflictMessages.Add(conflictMsg);
                        }
                    }
                }
            }
            foreach (var stateB in statesB)
            {
                if (stateB is IIncompatibleStates incompatibleB)
                {
                    var stateATypes = statesA.Select(s => s.GetType()).ToHashSet();

                    var conflictsWithA = stateATypes
                        .Where(typeA => incompatibleB.IncompatibleStates.Contains(typeA))
                        .ToList();

                    if (conflictsWithA.Count > 0)
                    {
                        hasConflicts = true;
                        foreach (var conflictingType in conflictsWithA)
                        {
                            string conflictMsg =
                                $"Состояние {stateB.GetType().Name} несовместимо с состоянием {conflictingType.Name}";

                            if (!conflictMessages.Contains(conflictMsg))
                                conflictMessages.Add(conflictMsg);
                        }
                    }
                }
            }
            if (hasConflicts)
            {
                throw new ArgumentException("Обнаружены несовместимые состояния. Проверьте логи.");
            }

            Debug.Log($"Проверка совместимости завершена успешно. Конфликтов не обнаружено.", context);
            return true;
        }
        public static T GetOrException<T>(this GameObject gameObject) where T : class =>
            gameObject.GetComponents<MonoBehaviour>().FirstOrDefault(b => b is T) as T
            ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static int GetTypeIndexInPriorityList(Type type, IReadOnlyList<Type> priorityTypes)
        {
            if (priorityTypes == null) return -1;

            // Ищем точное соответствие типа
            for (int i = 0; i < priorityTypes.Count; i++)
            {
                if (priorityTypes[i] == type)
                    return i;
            }

            // Если точного соответствия нет, ищем через наследование
            for (int i = 0; i < priorityTypes.Count; i++)
            {
                if (type.IsAssignableFrom(priorityTypes[i]))
                    return i;
            }

            // Если тип не найден в списке, возвращаем максимальное значение
            return int.MaxValue;
        }
    }
}