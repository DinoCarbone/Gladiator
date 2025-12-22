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
        [SerializeField] private int costKillable = 1;
        public override IState CreateConfigState(List<GameObject> contexts)
        {
           GameObject coreGameObject = null;

            if(contexts.Count > 0) coreGameObject = contexts[0];
            else
            throw new Exception("EntityDeathSO: CoreGameObject is empty");

            EnemyKillableData enemyKillable = new EnemyKillableData(coreGameObject, costKillable);

            return new DefautDeath(GetIncompatibleTypes(), enemyKillable);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseDeath);
        }
    }
}