using Core.Providers;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Agents
{
    public class EnemyAgetn : MovementAttackAgent
    {
        private Transform _targetTransform;
        protected override Transform targetTransform => _targetTransform;
        private IPlayerSceneProvider playerSceneProvider;
        public EnemyAgetn(Transform selfTransform, float attackAngleThreshold, float attackDistance) : base(selfTransform, attackAngleThreshold, attackDistance)
        {
        }
        [Inject]
        private void Construct(IPlayerSceneProvider playerSceneProvider)
        {
            this.playerSceneProvider = playerSceneProvider;
            _targetTransform = playerSceneProvider.Transform;
        }

    }
}