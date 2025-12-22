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

        public IHealthViewUpdater Create(IHealthService healthService)
        { 
            healthViewUpdater.Construct(healthService);
            return healthViewUpdater;
        }
    }
}