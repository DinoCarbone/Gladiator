using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers.Input
{
    public class InputAxisMovementProvider : IAxisMovementProvider, IDisposable
    {
        private IMovementInput inputAxisService;

        public bool IsHandle => inputAxisService.IsHandle;

        public Vector2 Axis => OnInputAxisChanged();

        [Inject]
        private void Construct(IMovementInput inputAxisService)
        {
            this.inputAxisService = inputAxisService;
        }

        private Vector2 OnInputAxisChanged()
        {
            Vector2 output = inputAxisService.Axis == Vector2.zero ? Vector2.zero : Vector2.up;
            return output;
        }

        public void Dispose()
        {
            inputAxisService = null;
        }
    }
}