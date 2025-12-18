using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers
{
    public class InputAxisMovementProvider : IAxisMovementProvider, IDisposable
    {
        private IMovementInput inputAxisService;

        public Vector2 Axis { get; private set; } = Vector2.zero;
        
        [Inject]
        private void Construct(IMovementInput inputAxisService)
        {
            this.inputAxisService = inputAxisService;
            this.inputAxisService.OnMovementAxisChanged += OnInputAxisChanged;
        }

        public event Action<Vector2> OnAxisChanged;

        private void OnInputAxisChanged(Vector2 axis)
        {
            Axis = axis == Vector2.zero ? axis : Vector2.up;
            OnAxisChanged?.Invoke(axis);
        }

        public void Dispose()
        {
            inputAxisService.OnMovementAxisChanged -= OnInputAxisChanged;
            inputAxisService = null;
        }
    }
}