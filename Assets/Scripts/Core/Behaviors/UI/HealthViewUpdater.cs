using Core.Behaviors.Health;
using Utils;
using Zenject;

namespace Core.Behaviors.UI
{
    /// <summary>
    /// Провайдер визуального обновления здоровья: синхронизирует <see cref="IValueDisplay"/> с данным <see cref="IHealthService"/>.
    /// </summary>
    public class HealthViewUpdater : Providers.IProvider, IHealthViewUpdater
    {
        private readonly IValueDisplay valueDisplay;
        private IHealthService healthProvider;

        public HealthViewUpdater(IValueDisplay valueDisplay)
        {
            this.valueDisplay = Extensions.AssignWithNullCheck(valueDisplay);
        }

        /// <summary>Инъекция сервиса здоровья и первичная инициализация отображения.</summary>
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

        /// <summary>Освобождает подписки и очищает ссылку на провайдер здоровья.</summary>
        public void Dispose()
        {
            Unsubscribe();
            healthProvider = null;
        }
    }
}