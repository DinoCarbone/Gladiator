using Core.Providers;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Agents
{
    public class EnemyAgent : MovementAttackAgent
    {
        private Transform _targetTransform;
        protected override Transform targetTransform => _targetTransform;

        private IPlayerSceneProvider playerSceneProvider;

        public EnemyAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
            : base(selfTransform, attackAngleThreshold, attackDistance)
        {
        }

            /// <summary>
            /// Выполняет внедрение провайдера сцены игрока и устанавливает цель агента.
            /// </summary>
            [Inject]
            private void Construct(IPlayerSceneProvider playerSceneProvider)
            {
                this.playerSceneProvider = playerSceneProvider;
                _targetTransform = playerSceneProvider.Transform;
            }
    }
}