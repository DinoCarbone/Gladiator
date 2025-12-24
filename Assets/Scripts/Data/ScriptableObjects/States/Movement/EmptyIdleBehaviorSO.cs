using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "EmptyIdle", 
    menuName = "ScriptableObjects/States/Movement/EmptyIdle")]
    /// <summary>
    /// ScriptableObject-конфигурация для состояния пустого простоя (EmptyIdle).
    /// Создаёт конфигурационное состояние `EmptyIdleState` на основе несовместимых типов.
    /// </summary>
    public class EmptyIdleBehaviorSO : BehaviorSO<EmptyIdleState>
    {
        /// <param name="contexts">Список GameObject-контекстов, может быть пустым.</param>
        /// <returns>Экземпляр <see cref="IState"/> представляющий конфигурацию пустого простоя.</returns>
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new EmptyIdleState(GetIncompatibleTypes());
        }

        /// <summary>
        /// Возвращает базовый тип поведения, с которым совместимо данное ScriptableObject.
        /// </summary>
        /// <returns>Тип базового поведения — <see cref="BaseIdle"/>.</returns>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseIdle);
        }
    }
}