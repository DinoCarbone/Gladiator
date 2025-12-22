using Core.Providers;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class PlayerKillableData : IPlayerKillableData{}
    public class EnemyKillableData : IEnemyKillableData
    {
        private readonly GameObject coreGameObject;
        public GameObject CoreGameObject => coreGameObject;
        private readonly int cost;
        public int Cost => cost;

        public EnemyKillableData(GameObject coreGameObject, int cost)
        {
            this.cost = Extensions.AssignWithZeroCheck(cost);
            this.coreGameObject = Extensions.AssignWithNullCheck(coreGameObject);
        }
    }
}