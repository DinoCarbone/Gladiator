using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseRotation",
    menuName = "ScriptableObjects/States/Base/BaseRotation")]
    public class BaseRotationBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseRotation(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseRotation);
        }
    }
}