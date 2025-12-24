using System;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Providers.Input
{
    public class InputAxisMovementProvider : IAxisMovementProvider, IDisposable
    {
        private IMovementInput inputAxisService;

        /// <summary>Возвращает true, когда есть активный ввод движения.</summary>
        public bool IsHandle => inputAxisService.IsHandle;

        /// <summary>Преобразованная ось движения для потребителей (направление вперед/назад).</summary>
        public Vector2 Axis => OnInputAxisChanged();

        [Inject]
        private void Construct(IMovementInput inputAxisService)
        {
            this.inputAxisService = inputAxisService;
        }

        /// <summary>
        /// Конвертирует входную ось в упрощённый вектор движения (вперёд/назад).
        /// </summary>
        /// <returns>Vector2.up при наличии ввода, иначе Vector2.zero.</returns>
        private Vector2 OnInputAxisChanged()
        {
            Vector2 output = inputAxisService.Axis == Vector2.zero ? Vector2.zero : Vector2.up;
            return output;
        }

        /// <summary>Освобождает ссылки на сервис ввода.</summary>
        public void Dispose()
        {
            inputAxisService = null;
        }
    }
}