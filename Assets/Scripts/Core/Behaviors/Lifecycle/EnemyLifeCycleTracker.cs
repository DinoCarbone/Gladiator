using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Отвечает за спавн и удаление врагов; подписывается на уведомления об их смерти и рекреирует новый экземпляр.
    /// </summary>
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

        /// <summary>Обрабатывает уведомление о смерти врага: удаляет его и создаёт нового.</summary>
        public void OnReciveDeath(IEnemyKillableData enemyKillableData)
        {
            Despawn(enemyKillableData.CoreGameObject);
            Spawn();
        }

        /// <summary>Удаляет врага из списка и уничтожает GameObject.</summary>
        public void Despawn(GameObject gameObject)
        {
            if (activeEnemies.Contains(gameObject))
            {
                activeEnemies.Remove(gameObject);
            }
            else Debug.LogWarning("Enemy not found");

            UnityEngine.Object.Destroy(gameObject);
        }

        /// <summary>Создаёт нового врага через фабрику и добавляет в список активных.</summary>
        public void Spawn()
        {
            activeEnemies.Add(enemyFactory.Create());
        }

        /// <summary>Очистка: уничтожает все созданные объекты и отписывается от уведомлений.</summary>
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