using System;
using UnityEngine;
using Zenject;

namespace Core.Services.Input
{
    public class DesktopInput : IInputAxisService, ITickable
    {
        public event Action<Vector2> OnAxisChanged;

        public void Tick()
        {
            Vector2 axis = new Vector2( UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            OnAxisChanged?.Invoke(axis);
        }
    }
}