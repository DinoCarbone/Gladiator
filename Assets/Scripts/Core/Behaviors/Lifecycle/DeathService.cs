using System;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class MockDeathService : IDeathService
    {
        public void RegisterDeath(IKillableData killable)
        {
            Debug.Log("RegisterDeath by type: " + killable.GetType().Name);
        }
    }
    public class DeathRouter : IDeathService, IEnemyDeathNotifier, IPlayerDeathNotifier
    {
        public event Action<IEnemyKillableData> OnEnemyDied;
        public event Action<IPlayerKillableData> OnPlaerDied;

        public void RegisterDeath(IKillableData killable)
        {
            if (killable is IPlayerKillableData playerKillableData)
            {
                OnPlaerDied?.Invoke(playerKillableData);
            }
            else if (killable is IEnemyKillableData enemyKillableData)
            {
                OnEnemyDied?.Invoke(enemyKillableData);
            }
            else Debug.LogError("Unknown killable type: " + killable.GetType().Name);
        }
    }
}