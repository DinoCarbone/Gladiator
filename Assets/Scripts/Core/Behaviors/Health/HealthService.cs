using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Serialization;
using UnityEngine;

namespace Core.Behaviors.Health
{
    public class HealthService : IInternalEventReceiver, IDamageProvider, IHealthService, IDeathProvider
    {
        /// <summary>Текущее здоровье.</summary>
        public int Health { get; private set; }

        /// <summary>Максимальное здоровье.</summary>
        public int MaxHealth { get; private set; }

        /// <summary>Событие при получении урона (возвращает величину урона).</summary>
        public event Action<int> OnTakeDamage;

        /// <summary>Событие при изменении здоровья (возвращает текущее здоровье).</summary>
        public event Action<int> OnChangeHealth;

        /// <summary>Событие при смерти (health <= 0).</summary>
        public event Action OnDie;

        /// <summary>Создаёт сервис здоровья с указанным максимумом.</summary>
        /// <param name="maxHealth">Максимальное здоровье.</param>
        public HealthService(int maxHealth)
        {
            if(maxHealth <= 0) Debug.LogError("Health Service Max Health Reached");
            
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void ReceiveEvent(IEvent @event)
        {
            if(@event is DamageData damageData)
            {
                ReceiveDamage(damageData.Damage);
            }
        }
        private void ReceiveDamage(int damage)
        {
            if(damage <= 0 || Health <= 0) return;

            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                OnDie?.Invoke();
            }
            OnTakeDamage?.Invoke(damage);
            OnChangeHealth?.Invoke(Health);
        }
    }
}