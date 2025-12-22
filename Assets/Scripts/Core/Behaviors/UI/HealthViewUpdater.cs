using Core.Behaviors.Health;
using Utils;
using Zenject;

namespace Core.Behaviors.UI
{
    public class HealthViewUpdater : Providers.IProvider, IHealthViewUpdater
    {
        private readonly IValueDisplay valueDisplay;
        private IHealthService healthProvider;

        public HealthViewUpdater(IValueDisplay valueDisplay)
        {
            this.valueDisplay = Extensions.AssignWithNullCheck(valueDisplay);
        }

        [Inject]
        public void Construct(IHealthService healthProvider)
        {
            this.healthProvider = Extensions.AssignWithNullCheck(healthProvider);

            valueDisplay.SetMaxValue(healthProvider.MaxHealth);
            valueDisplay.DisplayValue(healthProvider.Health);

            Subscribe();
        }
        private void Subscribe()
        {
            healthProvider.OnChangeHealth += OnChangeHealth;
        }
        private void Unsubscribe()
        {
            healthProvider.OnChangeHealth -= OnChangeHealth;
        }
        private void OnChangeHealth(int health)
        {
            valueDisplay.DisplayValue(health);
        }
        public void Dispose()
        {
            Unsubscribe();
            healthProvider = null;
        }
    }
}