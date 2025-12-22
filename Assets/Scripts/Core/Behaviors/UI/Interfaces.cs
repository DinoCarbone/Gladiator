using System;
using Core.Behaviors.Health;

namespace Core.Behaviors.UI
{
    public interface IValueDisplay
    {
        void SetMaxValue(int value);
        void DisplayValue(int value);
    }
    public interface IHealthViewUpdater : IDisposable{}
    public interface IHealthViewPlayerFactory
    {
        IHealthViewUpdater Create(IHealthService healthService);
    }
}