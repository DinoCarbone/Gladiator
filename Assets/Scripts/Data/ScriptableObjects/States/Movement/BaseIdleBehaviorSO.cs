using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "_BaseIdle",
    menuName = "ScriptableObjects/States/Base/BaseIdle")]
    public class BaseIdleBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseIdle(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseIdle);
        }
    }
}