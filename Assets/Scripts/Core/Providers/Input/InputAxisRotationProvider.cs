using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;
using Utils;

namespace Core.Providers.Input
{
    public class InputAxisRotationProvider : IAxisRotationProvider, IDisposable
    {
        private IMovementInput movementInput;
        private Transform cameraTransform;
        private readonly float rotationThreshold = 0.1f;
        private Quaternion rotation = Quaternion.identity;

        /// <summary>Текущее вращение, рассчитанное по оси движения и направлению камеры.</summary>
        public Quaternion Rotation => GetAxisRotation();

        /// <summary>
        /// Создаёт провайдер вращения оси с порогом чувствительности.
        /// </summary>
        /// <param name="rotationThreshold">Минимальная величина оси для обновления поворота.</param>
        public InputAxisRotationProvider(float rotationThreshold)
        {
            this.rotationThreshold = rotationThreshold;
        }

        [Inject]
        private void Construct(IMovementInput movementInput, ICameraProvider cameraProvider)
        {
            this.movementInput = movementInput;
            cameraTransform = Extensions.AssignWithNullCheck(cameraProvider.CameraTransform);
        }

        /// <summary>
        /// Вычисляет направление взгляда по текущей оси движения и ориентации камеры.
        /// </summary>
        /// <returns>Quaternion с направлением взгляда; сохраняется между вызовами.</returns>
        private Quaternion GetAxisRotation()
        {
            if (movementInput.Axis.magnitude > rotationThreshold)
            {
                Vector3 cameraForward = cameraTransform.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                Vector3 cameraRight = cameraTransform.right;
                cameraRight.y = 0;
                cameraRight.Normalize();

                Vector3 targetDirection = (cameraForward * movementInput.Axis.y +
                                            cameraRight * movementInput.Axis.x).normalized;

                rotation = Quaternion.LookRotation(targetDirection);
            }

            return rotation;
        }

        /// <summary>Освобождает ссылки на сервис ввода.</summary>
        public void Dispose()
        {
            movementInput = null;
        }
    }
}