using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers.Input
{
    /// <summary>
    /// Адаптер ввода атаки: предоставляет флаг `IsAttack` на основе внедряемого `IAttackInput`.
    /// </summary>
    public class InputAttackProvider : IAttackProvider, IDisposable
    {
        private IAttackInput attackInput;

        /// <summary>Возвращает информацию о совершении атаки в текущем кадре.</summary>
        public bool IsAttack => attackInput.IsAttack;

        /// <summary>Внедряет реализацию ввода атаки.</summary>
        [Inject]
        private void Construct(IAttackInput attackInput)
        {
            this.attackInput = attackInput;
        }

        /// <summary>Освобождает ссылку на входящий сервис.</summary>
        public void Dispose()
        {
            attackInput = null;
        }
    }
}