using System;
using UnityEngine;
using Zenject;

namespace Core.Services.Input
{
    public class DesktopInput : ITickable, IMovementInput, IMouseLookInput, IAttackInput
    {
        private const float PressThreshold = 0.5f;
        private const float ZeroThreshold = 0.01f;
        
        private Vector2 movementAxis;
        private Vector2 lookAxis;
        private float movementZeroTime;
        private float previousFireValue = 0f;

        private bool isAttackThisFrame = false;

        public bool IsHandle { get; private set; }
        public Vector2 Axis { get; private set; }
        public bool IsAttack => isAttackThisFrame;

        public event Action<Vector2> OnMovementAxisChanged;
        public event Action<Vector2> OnLookAxisChanged;
        public event Action OnFireReleased;
        public event Action OnFirePressed;

        public void Tick()
        {
            Vector2 newMovementAxis = new Vector2(
                UnityEngine.Input.GetAxis("Horizontal"),
                UnityEngine.Input.GetAxis("Vertical")
            );

            if (movementAxis != newMovementAxis)
            {
                movementAxis = newMovementAxis;

                if (newMovementAxis != Vector2.zero)
                {
                    movementZeroTime = 0f;
                    IsHandle = true;
                }
                else
                {
                    movementZeroTime = 0f;
                }
                Axis = newMovementAxis;
                OnMovementAxisChanged?.Invoke(newMovementAxis);
            }
            else if (movementAxis == Vector2.zero && IsHandle)
            {
                movementZeroTime += Time.deltaTime;

                if (movementZeroTime >= ZeroThreshold)
                {
                    IsHandle = false;
                }
            }

            Vector2 newLookAxis = new Vector2(
                UnityEngine.Input.GetAxis("Mouse X"),
                UnityEngine.Input.GetAxis("Mouse Y")
            );

            if (lookAxis != newLookAxis)
            {
                lookAxis = newLookAxis;
                OnLookAxisChanged?.Invoke(newLookAxis);
            }

            float currentFireValue = UnityEngine.Input.GetAxis("Fire1");

            isAttackThisFrame = previousFireValue < PressThreshold && currentFireValue >= PressThreshold;

            if (isAttackThisFrame)
            {
                OnFirePressed?.Invoke();
            }

            if (previousFireValue >= PressThreshold && currentFireValue < PressThreshold)
            {
                OnFireReleased?.Invoke();
            }

            previousFireValue = currentFireValue;
        }
    }
}