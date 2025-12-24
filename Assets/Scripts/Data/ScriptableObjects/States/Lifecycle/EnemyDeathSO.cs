using System;
using System.Collections.Generic;
using Core.Behaviors.Lifecycle;
using Core.Behaviors.States.Lifecycle;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Lifecycle
{
    [CreateAssetMenu(fileName = "EnemyDeath",
    menuName = "ScriptableObjects/States/Lifecycle/EnemyDeath")]
    public class EnemyDeathSO : BehaviorSO<DefautDeath>
    {
        [SerializeField, Tooltip("Points awarded for killing this enemy.")]
        private int costKillable = 1;
        /// <summary>Создаёт конфигурацию смерти врага с указанием стоимости и core GameObject.</summary>
        public override IState CreateConfigState(List<GameObject> contexts)
        {
           GameObject coreGameObject = null;

            if(contexts.Count > 0) coreGameObject = contexts[0];
            else
                throw new Exception("EntityDeathSO: CoreGameObject is empty");

            EnemyKillableData enemyKillable = new EnemyKillableData(coreGameObject, costKillable);

            return new DefautDeath(GetIncompatibleTypes(), enemyKillable);
        }

        /// <summary>Базовый тип поведения для состояний смерти.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDeath);
        }
    }
}