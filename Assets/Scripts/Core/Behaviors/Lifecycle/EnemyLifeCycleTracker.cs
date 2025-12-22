using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class EnemyLifeCycleTracker : IEnemySpawner, IDisposable
    {
        private readonly IEnemyDeathNotifier enemyDeathNotifier;
        private readonly IEnemyFactory enemyFactory;
        private readonly List<GameObject> activeEnemies = new List<GameObject>();

        public EnemyLifeCycleTracker(IEnemyFactory enemyFactory, IEnemyDeathNotifier enemyDeathNotifier)
        {
            this.enemyFactory = Extensions.AssignWithNullCheck(enemyFactory);
            this.enemyDeathNotifier = Extensions.AssignWithNullCheck(enemyDeathNotifier);
            Subscribe();
            Spawn();
        }

        private void Subscribe()
        {
            enemyDeathNotifier.OnEnemyDied += OnReciveDeath;
        }
        private void Unsubscribe()
        {
            enemyDeathNotifier.OnEnemyDied -= OnReciveDeath;
        }

        public void OnReciveDeath(IEnemyKillableData enemyKillableData)
        {
            Despawn(enemyKillableData.CoreGameObject);
            Spawn();
        }
        public void Despawn(GameObject gameObject)
        {
            if (activeEnemies.Contains(gameObject))
            {
                activeEnemies.Remove(gameObject);
                UnityEngine.Object.Destroy(gameObject);
            }
            else
            Debug.LogError("Enemy not found");
        }
        public void Spawn()
        {
            activeEnemies.Add(enemyFactory.Create());
        }

        public void Dispose()
        {
            foreach (var enemy in activeEnemies)
            {
                UnityEngine.Object.Destroy(enemy);
            }
            Unsubscribe();
        }
    }
}