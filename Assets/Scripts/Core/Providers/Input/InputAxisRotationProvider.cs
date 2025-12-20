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
        public Quaternion Rotation => GetAxisRotation();


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

        public void Dispose()
        {
            movementInput = null;
        }
    }
}