using System;
using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public interface IKillableData{}
    public interface IPlayerKillableData : IKillableData{}
    public interface IEnemyKillableData : IKillableData, IScoreCostData
    {
        public GameObject CoreGameObject { get; }
    }
    public interface IDeathService
    {
        void RegisterDeath(IKillableData killable);
    }
    public interface IEnemyDeathNotifier
    {
        event Action<IEnemyKillableData> OnEnemyDied;
    }
    public interface IPlayerDeathNotifier
    {
        event Action<IPlayerKillableData> OnPlaerDied;
    }
    public interface IEnemySpawner
    {
        void Spawn();
        void Despawn(GameObject gameObject);
    }
    public interface IScoreAdder
    {
        void AddScore(IScoreCostData scoreCostData);
    }
    public interface IEnemyFactory
    {
        public GameObject Create();
    }
}