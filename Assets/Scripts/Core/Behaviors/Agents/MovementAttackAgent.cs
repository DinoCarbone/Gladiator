using Core.Providers;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Agents
{
    public abstract class MovementAttackAgent : IAttackProvider, IAxisMovementProvider, IAxisRotationProvider
    {
        private readonly Transform selfTransform;
        protected abstract Transform targetTransform { get; }
        private readonly float attackAngleThreshold;
        private readonly float attackDistance;

        public MovementAttackAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
        {
            this.selfTransform = Extensions.AssignWithNullCheck(selfTransform);
            this.attackAngleThreshold = attackAngleThreshold;
            this.attackDistance = attackDistance;
        }
        public bool IsAttack => CalculateIsAttack();
        public bool IsHandle => CalculateIsHandle();
        public Vector2 Axis => CalculateMovementAxis();
        public Quaternion Rotation => CalculateRotation();
        private Vector3 GetAgentPosition()
        {
            return selfTransform?.position ?? Vector3.zero;
        }

        private Quaternion GetAgentRotation()
        {
            return selfTransform?.rotation ?? Quaternion.identity;
        }

        private bool CalculateIsAttack()
        {
            var distance = Vector3.Distance(GetAgentPosition(), targetTransform.position);
            return distance <= attackDistance && IsLookingAtPlayer();
        }

        private bool CalculateIsHandle()
        {
            var distance = Vector3.Distance(GetAgentPosition(), targetTransform.position);
            return distance > attackDistance;
        }

        private Vector2 CalculateMovementAxis()
        {
            if (!IsHandle) return Vector2.zero;
            return Vector2.up;
        }

        private Quaternion CalculateRotation()
        {
            var direction = (targetTransform.position - GetAgentPosition()).normalized;
            if (direction == Vector3.zero) return GetAgentRotation();

            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            var currentRotation = GetAgentRotation();

            var angle = Quaternion.Angle(currentRotation, targetRotation);
            if (angle <= attackAngleThreshold)
            {
                return targetRotation;
            }

            return Quaternion.RotateTowards(currentRotation, targetRotation, attackAngleThreshold);
        }

        private bool IsLookingAtPlayer()
        {
            var directionToPlayer = (targetTransform.position - GetAgentPosition()).normalized;
            var forward = selfTransform?.forward ?? Vector3.forward;

            var angle = Vector3.Angle(forward, directionToPlayer);
            return angle <= attackAngleThreshold;
        }
    }
}