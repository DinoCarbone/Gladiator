using System;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Attack
{
    [CreateAssetMenu(fileName = "_BaseAttack",
    menuName = "ScriptableObjects/States/Base/BaseAttack")]
    public class BaseAttackBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseAttack(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}