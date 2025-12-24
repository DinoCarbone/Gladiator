using System;
using System.Collections.Generic;
using Core.Behaviors.Lifecycle;
using Core.Behaviors.States.Lifecycle;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Lifecycle
{
    [CreateAssetMenu(fileName = "PlayerDeath",
    menuName = "ScriptableObjects/States/Lifecycle/PlayerDeath")]
    public class PlayerDeathSO : BehaviorSO<DefautDeath>
    {
        /// <summary>Создаёт конфигурацию смерти игрока.</summary>
        public override IState CreateConfigState(List<GameObject> _)
        {
            PlayerKillableData playerKullable = new PlayerKillableData();

            return new DefautDeath(GetIncompatibleTypes(), playerKullable);
        }

        /// <summary>Возвращает базовый тип поведения для состояний смерти.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDeath);
        }
    }
}