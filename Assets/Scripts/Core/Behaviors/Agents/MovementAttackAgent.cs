using Core.Providers;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Agents
{
    /// <summary>
    /// Базовый агент, объединяющий логику движения и атаки относительно целевого трансформа.
    /// Предоставляет свойства для определения, должен ли агент двигаться или атаковать, и вычисляет поворот.
    /// </summary>
    public abstract class MovementAttackAgent : IAttackProvider, IAxisMovementProvider, IAxisRotationProvider
    {
        private readonly Transform selfTransform;
        protected abstract Transform targetTransform { get; }
        private readonly float attackAngleThreshold;
        private readonly float attackDistance;

        /// <summary>
        /// Создаёт агента с ссылкой на свой трансформ и параметрами атаки.
        /// </summary>
        /// <param name="selfTransform">Трансформ агента.</param>
        /// <param name="attackAngleThreshold">Порог угла для определения направления атаки.</param>
        /// <param name="attackDistance">Дистанция, на которой возможна атака.</param>
        public MovementAttackAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
        {
            this.selfTransform = Extensions.AssignWithNullCheck(selfTransform);
            this.attackAngleThreshold = attackAngleThreshold;
            this.attackDistance = attackDistance;
        }

        /// <summary>Возвращает true, если в текущий момент должна происходить атака.</summary>
        public bool IsAttack => CalculateIsAttack();

        /// <summary>Возвращает true, если агент должен управляться движением (не находится в радиусе атаки).</summary>
        public bool IsHandle => CalculateIsHandle();

        /// <summary>Вектор движения для внешних потребителей (упрощённый вектор вперед/стоп).</summary>
        public Vector2 Axis => CalculateMovementAxis();

        /// <summary>Требуемая ориентация агента для движения/атаки.</summary>
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