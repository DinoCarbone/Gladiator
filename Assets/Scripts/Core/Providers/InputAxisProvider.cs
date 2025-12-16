using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers
{
    public class InputAxisProvider : IAxisProvider, IDisposable
    {
        private IInputAxisService inputAxisService;

        public Vector2 Axis { get; private set; } = Vector2.zero;
        
        [Inject]
        private void Construct(IInputAxisService inputAxisService)
        {
            Debug.Log("InputAxisProvider Constructed");
            this.inputAxisService = inputAxisService;
            this.inputAxisService.OnAxisChanged += OnInputAxisChanged;
        }

        public event Action<Vector2> OnAxisChanged;

        private void OnInputAxisChanged(Vector2 axis)
        {
            Axis = axis;
            OnAxisChanged?.Invoke(axis);
        }

        public void Dispose()
        {
            inputAxisService.OnAxisChanged -= OnInputAxisChanged;
        }
    }
}