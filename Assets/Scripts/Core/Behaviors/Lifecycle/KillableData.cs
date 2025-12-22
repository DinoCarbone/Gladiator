using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class BaseKillableData : IKillableData
    {
        private readonly GameObject coreGameObject;
        public GameObject CoreGameObject => coreGameObject;
        protected BaseKillableData(GameObject coreGameObject)
        {
            this.coreGameObject = Extensions.AssignWithNullCheck(coreGameObject);
        }
    }
    public class PlayerKillableData : BaseKillableData
    {
        public PlayerKillableData(GameObject coreGameObject) : base(coreGameObject)
        {
        }
    }
    public class EnemyKillableData : BaseKillableData
    {
        public EnemyKillableData(GameObject coreGameObject) : base(coreGameObject)
        {
        }
    }
}