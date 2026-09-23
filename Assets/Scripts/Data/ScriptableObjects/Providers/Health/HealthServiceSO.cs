using Core.Behaviors.Health;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Health
{
    [CreateAssetMenu(fileName = "HealthService",
      menuName = "ScriptableObjects/Providers/Health/HealthService")]
    public class HealthServiceSO : BaseProviderSO
    {
        [SerializeField, Tooltip("Maximum health value for the health service.")]
        private int maxHealth = 100;
        public override IProvider CreateProvider(params object[] _)
        {
            return new HealthService(maxHealth);
        }
    }
}