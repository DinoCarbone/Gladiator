using System;
using Core.Behaviors.States.Lifecycle;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Lifecycle
{
    /// <summary>
    /// Используется только для прокидывания в списки несовместимых состояний.
    /// </summary>
    [CreateAssetMenu(fileName = "_BaseDeath",
    menuName = "ScriptableObjects/States/Base/BaseDeath")]
    public class BaseDeathSO : BaseBehaviorSO
    {
        public override IState CreateConfigState(params object[] dependencies)
        {
            return new BaseDeath(null);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDeath);
        }
    }
}