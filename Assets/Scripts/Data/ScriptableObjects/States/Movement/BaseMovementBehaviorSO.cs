using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "_BaseMovement",
    menuName = "ScriptableObjects/States/Base/BaseMovement")]
    public class BaseMovementBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseMovement(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseMovement);
        }
    }
}