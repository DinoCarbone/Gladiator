using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "EmptyIdle", 
    menuName = "ScriptableObjects/States/Movement/EmptyIdle")]
    public class EmptyIdleBehaviorSO : BehaviorSO<EmptyIdleState>
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new EmptyIdleState(GetIncompatibleTypes());
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseIdle);
        }
    }
}