using System;

namespace Core.Behaviors.Health
{
    public interface IHealthService
    {
        int MaxHealth { get; }
        int Health { get; }
        event Action<int> OnChangeHealth;
    }
}