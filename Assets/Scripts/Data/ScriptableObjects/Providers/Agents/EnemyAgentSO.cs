using System;
using System.Collections.Generic;
using Core.Behaviors.Agents;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Agents
{
    [CreateAssetMenu(fileName = "EnemyAgent",
      menuName = "ScriptableObjects/Providers/Agets/EnemyAgent")]
    public class EnemyAgentSO : BaseProviderSO
    {
        [SerializeField] private float attackAngleThreshold = 30f;
        [SerializeField] private float attackDistance = 1.7f;
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
             Transform transform = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out transform))
                    break;
            }
            if (transform == null)
                throw new Exception($"EnemyAgetnSO: Transform is empty");

            return new EnemyAgetn(transform, attackAngleThreshold, attackDistance);
        }
    }
}