using System;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Attack
{
    [CreateAssetMenu(fileName = "DefautAttack",
     menuName = "ScriptableObjects/States/Attack/DefautAttack")]
    public class DefautAttackBehaviorSO : BehaviorSO<DefautAttack>
    {
        public override IState CreateConfigState(params object[] contexts)
        {
            return new DefautAttack(GetIncompatibleTypes());
        }
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}