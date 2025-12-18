using System;
using System.Collections.Generic;
using System.Linq;
using Data.Serialization;
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
        public static T GetOrException<T>(this GameObject gameObject) where T : class =>
              gameObject.GetComponents<MonoBehaviour>().FirstOrDefault(b => b is T) as T
              ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static bool ContainsType<T>(this IEnumerable<T> collection, object typeInstance)
    where T : class
        {
            if (typeInstance == null) return false;

            Type targetType = typeInstance.GetType();
            return collection.Any(item => targetType.IsInstanceOfType(item));
        }
        public static AnimationStateTypeData FindCompatibleBehaviorType(
    object obj,
    List<AnimationStateTypeData> animationStates)
        {
            if (obj == null || animationStates == null)
                return null;

            Type objType = obj.GetType();

            foreach (var stateData in animationStates)
            {
                if (stateData.BehaviorType != null)
                {
                    // Проверяем оба направления совместимости
                    if (stateData.BehaviorType.IsAssignableFrom(objType) ||
                        objType.IsAssignableFrom(stateData.BehaviorType))
                    {
                        return stateData; // Возвращаем найденный AnimationStateTypeData
                    }
                }
            }

            return null; // Ничего не нашли
        }

    }
}