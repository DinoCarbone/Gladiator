using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Serialization;

namespace Core.Behaviors.Health
{
    public class HealthService : IInternalEventReceiver, IDamageProvider
    {
        public event Action<int> OnTakeDamage;

        public void ReceiveEvent(IEvent @event)
        {
            if(@event is DamageData damageData)
            {
                OnTakeDamage?.Invoke(damageData.Damage);
            }
        }
    }
}