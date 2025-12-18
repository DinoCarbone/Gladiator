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
    [CreateAssetMenu(fileName = "_BaseRotation", menuName = "ScriptableObjects/Behaviors/Base/BaseRotation")]
    public class BaseRotationBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new BaseRotation(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseRotation);
        }
    }
}