using System;
using System.Collections.Generic;
using System.Linq;
using Data.Dto;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Проверяет значение на null и логирует ошибку при отсутствии объекта.
        /// Возвращает переданное значение для удобного присваивания.
        /// </summary>
        /// <typeparam name="T">Тип ссылки.</typeparam>
        /// <param name="value">Проверяемое значение.</param>
        /// <returns>То же значение, если не null.</returns>
        public static T AssignWithNullCheck<T>(T value) where T : class
        {
            if (value == null)
            {
                string errorMessage = $"Value of type {typeof(T).Name} cannot be null";

                Debug.LogError(errorMessage);
            }

            return value;
        }
        /// <summary>
        /// Проверяет целочисленное значение на равенство нулю и логирует ошибку при нарушении.
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        /// <param name="parameterName">Необязательное имя параметра для сообщений об ошибке.</param>
        /// <returns>Исходное значение.</returns>
        public static int AssignWithZeroCheck(int value, string parameterName = null)
        {
            if (value == 0)
            {
                string errorMessage = string.IsNullOrEmpty(parameterName)
                    ? $"Value cannot be zero (type: int)"
                    : $"Parameter '{parameterName}' cannot be zero";

                Debug.LogError(errorMessage);
            }

            return value;
        }
          /// <summary>
          /// Возвращает компонент типа T из GameObject или бросает исключение, если не найден.
          /// </summary>
          /// <typeparam name="T">Ожидаемый тип компонента.</typeparam>
          /// <param name="gameObject">Исходный GameObject.</param>
          /// <returns>Найденный компонент типа T.</returns>
          public static T GetOrException<T>(this GameObject gameObject) where T : class =>
              gameObject.GetComponents<MonoBehaviour>().FirstOrDefault(b => b is T) as T
              ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        /// <summary>
        /// Проверяет, содержит ли коллекция элементы, совместимые по типу с переданным экземпляром.
        /// </summary>
        /// <typeparam name="T">Элемент коллекции.</typeparam>
        /// <param name="collection">Коллекция для проверки.</param>
        /// <param name="typeInstance">Экземпляр для сравнения типов.</param>
        /// <returns>True, если найден совместимый элемент.</returns>
        public static bool ContainsType<T>(this IEnumerable<T> collection, object typeInstance)
    where T : class
        {
            if (typeInstance == null) return false;

            Type targetType = typeInstance.GetType();
            return collection.Any(item => targetType.IsInstanceOfType(item));
        }
        /// <summary>
        /// Находит совместимый AnimationStateTypeData для объекта по типу поведения.
        /// Возвращает первый подходящий элемент или null.
        /// </summary>
        /// <param name="obj">Объект для проверки.</param>
        /// <param name="animationStates">Список возможных состояний-анимаций.</param>
        /// <returns>Найденный AnimationStateTypeData или null.</returns>
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
        /// <summary>
        /// Преобразует список данных переопределения blend-time в словарь по имени состояния.
        /// </summary>
        /// <param name="overrideBlendTimeDatas">Список данных переопределений.</param>
        /// <returns>Словарь с ключом - именем состояния, значением - временем blend.</returns>
        public static Dictionary<string, float> GetOverrideBlendTimes(List<OverrideBlendTimeData> overrideBlendTimeDatas)
        {
             Dictionary<string, float> overrideBlendTimes = null;
                if (overrideBlendTimeDatas?.Count > 0)
                {
                    overrideBlendTimes = new Dictionary<string, float>();
                    foreach (var overrideBlend in overrideBlendTimeDatas)
                    {
                        if(!overrideBlendTimes.ContainsKey(overrideBlend.animationStateSO.StateName))
                            overrideBlendTimes.Add(overrideBlend.animationStateSO.StateName , overrideBlend.overrideBlendTime);
                        else Debug.LogError("Duplicate key in overrideBlendTimeDatas");
                    }
                }
                return overrideBlendTimes;
        }

    }
}