using System;
using System.Collections.Generic;
using Core.Behaviors.States.Interaction;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Interaction
{
    [CreateAssetMenu(fileName = "EmptyDamage",
    menuName = "ScriptableObjects/States/Interaction/EmptyDamage")]
    public class EmptyDamageSO : BehaviorSO<BaseDamage>
    {
        public override IState CreateConfigState(List<GameObject> _)
        {
            return new EmptyDamage(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDamage);
        }
    }
}