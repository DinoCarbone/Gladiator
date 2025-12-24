using Core.Behaviors.Health;

namespace Core.Behaviors.UI
{
    public class HealthViewPlayerFactory : IHealthViewPlayerFactory
    {
        private HealthViewUpdater healthViewUpdater;

        public HealthViewPlayerFactory(IValueDisplay valueDisplay)
        {
            healthViewUpdater = new HealthViewUpdater(valueDisplay);
        }

        /// <summary>
        /// Создаёт и возвращает инстанс `IHealthViewUpdater` для конкретного сервиса здоровья.
        /// </summary>
        public IHealthViewUpdater Create(IHealthService healthService)
        {
            healthViewUpdater.Construct(healthService);
            return healthViewUpdater;
        }
    }
}