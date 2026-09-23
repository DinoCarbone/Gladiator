using System;
using Core.Behaviors.States.Interaction;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Interaction
{
    [CreateAssetMenu(fileName = "_BaseDamage",
    menuName = "ScriptableObjects/States/Base/BaseDamage")]
    public class BaseDamageSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseDamage(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDamage);
        }
    }
}