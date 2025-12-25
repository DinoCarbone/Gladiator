using System;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Attack
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseAttack", 
    menuName = "ScriptableObjects/States/Base/BaseAttack")]
    public class BaseAttackBehaviorSO : BaseBehaviorSO
    {
        /// <summary>
        /// Создаёт конфигурационное состояние для данного ScriptableObject.
        /// </summary>
        /// <param name="contexts">Список контекстных GameObject, если требуется.</param>
        /// <returns>Экземпляр <see cref="IState"/>, представляющий базовое состояние атаки.</returns>
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseAttack(null);
        }

        /// <summary>
        /// Возвращает базовый тип поведения, используемый для группировки и совместимости состояний.
        /// </summary>
        /// <returns>Тип базового состояния атаки.</returns>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}