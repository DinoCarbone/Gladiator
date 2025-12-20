using System;
using System.Collections.Generic;
using Core.Behaviors.States.Interaction;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors.Interaction
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseDamage",
    menuName = "ScriptableObjects/Behaviors/Base/BaseDamage")]
    public class BaseDamageSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(List<GameObject> _)
        {
            return new BaseDamage(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDamage);
        }
    }
}