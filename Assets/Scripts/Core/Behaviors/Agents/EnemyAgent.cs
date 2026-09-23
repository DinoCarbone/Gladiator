using Core.Providers;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Agents
{
    public class EnemyAgent : MovementAttackAgent
    {
        private Transform _targetTransform;
        public override Transform TargetTransform => _targetTransform;


        public EnemyAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
            : base(selfTransform, attackAngleThreshold, attackDistance)
        {
        }

            [Inject]
            public void Construct(IPlayerSceneProvider playerSceneProvider)
            {
                _targetTransform = playerSceneProvider.Transform;
            }
    }
}