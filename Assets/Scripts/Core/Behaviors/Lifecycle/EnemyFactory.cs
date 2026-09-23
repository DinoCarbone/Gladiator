using UnityEngine;
using Zenject;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer container;
        private readonly GameObject prefab;
        private readonly Transform spawnPoint;

        public EnemyFactory(GameObject prefab, Transform spawnPoint, DiContainer container)
        {
            this.prefab = Extensions.AssignWithNullCheck(prefab);
            this.container = Extensions.AssignWithNullCheck(container);
            this.spawnPoint = spawnPoint;
        }

        public GameObject Create()
        {
            var enemy = container.InstantiatePrefab(
                prefab,
                spawnPoint.position,
                spawnPoint.rotation,
                null
            );

            return enemy;
        }
    }
}