using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseIdle", 
    menuName = "ScriptableObjects/States/Base/BaseIdle")]
    public class BaseIdleBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseIdle(null);
        }

        /// <summary>Возвращает базовый тип для idle-состояний.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseIdle);
        }
    }
}