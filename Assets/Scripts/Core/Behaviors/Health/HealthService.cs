using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Serialization;

namespace Core.Behaviors.Health
{
    public class HealthService : IInternalEventReceiver, IDamageProvider, IHealthService, IDeathProvider
    {
        public int Health {get; private set;}

        public int MaxHealth {get; private set;}

        public event Action<int> OnTakeDamage;
        public event Action<int> OnChangeHealth;
        public event Action OnDie;

        public HealthService(int maxHealth)
        {
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
            if(damage <= 0) return;

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