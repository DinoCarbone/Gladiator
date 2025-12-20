using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers.Input
{
    public class InputAttackProvider : IAttackProvider, IDisposable
    {
        private IAttackInput attackInput;
        public bool IsAttack => attackInput.IsAttack;

        [Inject]
        private void Construct(IAttackInput attackInput)
        {
            this.attackInput = attackInput;
        }
        public void Dispose()
        {
            attackInput = null;
        }
    }
}