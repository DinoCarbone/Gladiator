using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;
using Utils;

namespace Core.Providers
{
    public class InputAxisRotationProvider : IAxisRotationProvider, IDisposable
    {
        private IMovementInput movementInput;
        private Transform cameraTransform;
        private readonly float rotationThreshold = 0.1f;

        public Quaternion Rotation { get; private set; } = Quaternion.identity;
        public event Action<Quaternion> OnAxisRotation;


        public InputAxisRotationProvider(float rotationThreshold)
        {
            this.rotationThreshold = rotationThreshold;
        }

        [Inject]
        private void Construct(IMovementInput movementInput, ICameraProvider cameraProvider)
        {
            this.movementInput = movementInput;
            movementInput.OnMovementAxisChanged += OnInputAxisChanged;
            cameraTransform = Extensions.AssignWithNullCheck(cameraProvider.CameraTransform);
        }

        private void OnInputAxisChanged(Vector2 axis)
        {
            if (axis.magnitude > rotationThreshold)
            {
                Vector3 cameraForward = cameraTransform.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                Vector3 cameraRight = cameraTransform.right;
                cameraRight.y = 0;
                cameraRight.Normalize();

                Vector3 targetDirection = (cameraForward * axis.y + cameraRight * axis.x).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                Rotation = targetRotation;
                OnAxisRotation?.Invoke(targetRotation);
            }
        }

        public void Dispose()
        {
            movementInput.OnMovementAxisChanged -= OnInputAxisChanged;
            movementInput = null;
        }
    }
}