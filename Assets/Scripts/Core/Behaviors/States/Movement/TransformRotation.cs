using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Behaviors.States.Movement
{
    public class TransformRotation : BaseAxisRotation
    {
        private float rotationSpeed = 10;
        private Transform rootTransform;
        /// <summary>
        /// Конструктор состояния плавного вращения трансформа к целевой ротации.
        /// </summary>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        /// <param name="rootTransform">Transform, который будет вращаться.</param>
        /// <param name="rotationSpeed">Скорость интерполяции поворота.</param>
        public TransformRotation(List<Type> incompatibleStates, Transform rootTransform,
         float rotationSpeed) : base(incompatibleStates)
        {
            this.rootTransform = Extensions.AssignWithNullCheck(rootTransform);
            this.rotationSpeed = rotationSpeed;
        }

        /// <summary>
        /// Применяет сглаженную ротацию к <see cref="rootTransform"/> в направлении <paramref name="targetRotation"/>.
        /// </summary>
        /// <param name="targetRotation">Целевая ротация, к которой производится сглаживание.</param>
        protected override void OnRotation(Quaternion targetRotation)
        {
            rootTransform.rotation = Quaternion.Slerp(
                rootTransform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}