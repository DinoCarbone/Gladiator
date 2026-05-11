using System;
using Core.Behaviors.Agents;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Agents
{
    /// <summary>
    /// ScriptableObject-конфиг для создания `EnemyAgent` провайдера.
    /// Хранит параметры атаки (угол, дистанция) и создаёт провайдер по контекстам.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyAgent",
      menuName = "ScriptableObjects/Providers/Agets/EnemyAgent")]
    public class EnemyAgentSO : BaseProviderSO
    {
        [SerializeField, Tooltip("Attack angle threshold in degrees.")]
        private float attackAngleThreshold = 30f;

        [SerializeField, Tooltip("Maximum attack distance.")]
        private float attackDistance = 1.7f;

        /// <summary>Создаёт провайдер агента по списку контекстов (ищет Transform).</summary>
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Transform transform = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                transform = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }

            if (transform == null)
                throw new Exception($"EnemyAgentSO: Transform is empty");

            return new EnemyAgent(transform, attackAngleThreshold, attackDistance);
        }

        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Core Transform",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}