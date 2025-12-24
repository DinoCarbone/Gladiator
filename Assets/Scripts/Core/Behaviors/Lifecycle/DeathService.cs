using System;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class MockDeathService : IDeathService
    {
        /// <summary>Тестовая реализация сервиса смерти — логирует регистрацию смертей.</summary>
        public void RegisterDeath(IKillableData killable)
        {
            Debug.Log("RegisterDeath by type: " + killable.GetType().Name);
        }
    }
    public class DeathRouter : IDeathService, IEnemyDeathNotifier, IPlayerDeathNotifier
    {
        /// <summary>Событие — враг умер.</summary>
        public event Action<IEnemyKillableData> OnEnemyDied;

        /// <summary>Событие — игрок умер.</summary>
        public event Action<IPlayerKillableData> OnPlaerDied;

        /// <summary>Регистрирует смерть и ретранслирует событие в зависимости от типа IKillableData.</summary>
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