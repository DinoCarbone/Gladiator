using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors.Movement
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseMovement", menuName = "ScriptableObjects/Behaviors/Base/BaseMovement")]
    public class BaseMovementBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new BaseMovement(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseMovement);
        }
    }
}