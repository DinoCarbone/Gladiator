using System.Collections.Generic;
using Core.Behaviors.Health;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Health
{
    [CreateAssetMenu(fileName = "HealthService",
      menuName = "ScriptableObjects/Providers/Health/HealthService")]
    public class HealthServiceSO : BaseProviderSO
    {
        [SerializeField] private int maxHealth = 100;
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new HealthService(maxHealth);
        }
    }
}