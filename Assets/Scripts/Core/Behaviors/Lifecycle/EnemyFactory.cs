using UnityEngine;
using Zenject;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Фабрика создания врагов с использованием Zenject контейнера.
    /// </summary>
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

        /// <summary>Создаёт новый объект врага в точке спавна через DiContainer.</summary>
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