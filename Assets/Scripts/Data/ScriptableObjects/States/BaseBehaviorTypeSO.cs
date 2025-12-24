using System;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
    public abstract class BaseBehaviorTypeSO : ScriptableObject
    {
        /// <summary>
        /// Возвращает базовый тип поведения, с которым соотносится данный ScriptableObject.
        /// Используется для сопоставления состояний анимации и поведений.
        /// </summary>
        public abstract Type GetBaseBehaviorType();
    }
}