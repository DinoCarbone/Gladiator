using System;
using Core.Behaviors.Lifecycle;
using Core.Behaviors.States.Lifecycle;
using Core.Services.States;
using Data.Serialization;
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
        public override IState CreateConfigState(params object[] dependencies)
        {
           GameObject coreGameObject = null;

            if(dependencies != null && dependencies.Length > 0) coreGameObject = dependencies[0] as GameObject ?? (dependencies[0] as Component)?.gameObject;
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
        
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Core GameObject",
                    typeName = "",
                    optional = false
                }
            };
        }
    }
}