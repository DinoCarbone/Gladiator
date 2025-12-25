using System;
using Core.Behaviors.States.Interaction;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Interaction
{
    [CreateAssetMenu(fileName = "EmptyDamage",
    menuName = "ScriptableObjects/States/Interaction/EmptyDamage")]
    /// <summary>
    /// ScriptableObject-конфигурация для пустого состояния урона (EmptyDamage).
    /// Создаёт конфигурационное состояние `EmptyDamage`.
    /// </summary>
    public class EmptyDamageSO : BehaviorSO<BaseDamage>
    {
        /// <summary>
        /// Создаёт экземпляр конфигурационного состояния `EmptyDamage`.
        /// </summary>
        /// <param name="_">Контексты, не используются.</param>
        /// <returns>Экземпляр <see cref="IState"/>.</returns>
        public override IState CreateConfigState(params object[] _)
        {
            return new EmptyDamage(null);
        }

        /// <summary>
        /// Возвращает базовый тип поведения, соответствующий этому ScriptableObject.
        /// </summary>
        /// <returns>Тип базового поведения — <see cref="BaseDamage"/>.</returns>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDamage);
        }
    }
}