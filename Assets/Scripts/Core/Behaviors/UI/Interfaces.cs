using System;
using Core.Behaviors.Health;

namespace Core.Behaviors.UI
{
    public interface IValueDisplay
    {
        /// <summary>Устанавливает максимальное значение для отображения (например, максимальное здоровье).</summary>
        void SetMaxValue(int value);

        /// <summary>Обновляет отображаемое значение.</summary>
        void DisplayValue(int value);
    }
    /// <summary>Обновляет представление здоровья (View) в ответ на изменения сервиса здоровья.</summary>
    public interface IHealthViewUpdater : IDisposable { }

    /// <summary>Фабрика, создающая `IHealthViewUpdater` для конкретного `IHealthService`.</summary>
    public interface IHealthViewPlayerFactory
    {
        /// <summary>Создаёт `IHealthViewUpdater`, привязанный к переданному сервису здоровья.</summary>
        IHealthViewUpdater Create(IHealthService healthService);
    }
}