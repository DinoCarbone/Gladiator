using System;
using UnityEngine;
using Zenject;

namespace Core.Services.Input
{
    public class DesktopInput : IMovementInput, IMouseLookInput, ITickable
    {
        public event Action<Vector2> OnMovementAxisChanged;
        public event Action<Vector2> OnLookAxisChanged;

        public void Tick()
        {
            Vector2 axis = new Vector2( UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            OnMovementAxisChanged?.Invoke(axis);

            Vector2 lookAxis = new Vector2( UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
            OnLookAxisChanged?.Invoke(lookAxis);
        }
    }
}