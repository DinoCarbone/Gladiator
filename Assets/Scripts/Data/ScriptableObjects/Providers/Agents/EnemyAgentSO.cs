using System;
using System.Collections.Generic;
using Core.Behaviors.Agents;
using Core.Providers;
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
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
             Transform transform = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out transform))
                    break;
            }
            if (transform == null)
                throw new Exception($"EnemyAgentSO: Transform is empty");

            return new EnemyAgent(transform, attackAngleThreshold, attackDistance);
        }
    }
}