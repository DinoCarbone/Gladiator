using System;
using System.Collections.Generic;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors.Attack
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseAttack", menuName = "ScriptableObjects/Behaviors/Base/BaseAttack")]
    public class BaseAttackBehaviorSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new BaseAttack(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}