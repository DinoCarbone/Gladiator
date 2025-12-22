using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public interface IDeathService
    {
        void RegisterDeath(IKillableData killable);
    }
    public interface IKillableData
    {
        GameObject CoreGameObject { get; }
    }
}