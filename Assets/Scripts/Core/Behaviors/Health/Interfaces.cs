using System;

namespace Core.Behaviors.Health
{
    public interface IHealthService
    {
        /// <summary>Максимальное значение здоровья.</summary>
        int MaxHealth { get; }

        /// <summary>Текущее значение здоровья.</summary>
        int Health { get; }

        /// <summary>Событие — здоровье изменилось (передаётся новое значение).</summary>
        event Action<int> OnChangeHealth;
    }
}