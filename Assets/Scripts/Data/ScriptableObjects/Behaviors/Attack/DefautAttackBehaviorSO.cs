using System;
using System.Collections.Generic;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors.Attack
{
    [CreateAssetMenu(fileName = "DefautAttack", menuName = "ScriptableObjects/Behaviors/Attack/DefautAttack")]
    public class DefautAttackBehaviorSO : BehaviorSO<DefautAttack>
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new DefautAttack(GetIncompatibleTypes());
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}