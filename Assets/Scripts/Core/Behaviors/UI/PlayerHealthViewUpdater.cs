using System;
using Zenject;
using Core.Behaviors.Health;

namespace Core.Behaviors.UI
{
    public class PlayerHealthViewUpdater : Providers.IProvider, IDisposable
    {
        private IHealthViewUpdater healthViewUpdater;
        
        /// <summary>Фасад для создания и управления обновлением отображения здоровья игрока.</summary>
        /// <remarks>Создаётся через провайдеры в контейнере.</remarks>
        [Inject]
        private void Construct(IHealthViewPlayerFactory factory, IHealthService healthService)
        {
            healthViewUpdater = factory.Create(healthService);
        }

        /// <summary>Освобождает внутренний `IHealthViewUpdater`.</summary>
        public void Dispose()
        {
            healthViewUpdater.Dispose();
        }

    }
}