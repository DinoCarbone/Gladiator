using System;
using Zenject;
using Core.Behaviors.Health;

namespace Core.Behaviors.UI
{
    public class PlayerHealthViewUpdater : Providers.IProvider, IDisposable
    {
        private IHealthViewUpdater healthViewUpdater;
        [Inject]
        private void Construct(IHealthViewPlayerFactory factory, IHealthService healthService)
        {
            healthViewUpdater = factory.Create(healthService);
        }
        public void Dispose()
        {
            healthViewUpdater.Dispose();
        }

    }
}