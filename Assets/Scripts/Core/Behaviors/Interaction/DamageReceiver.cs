using System;
using Core.Providers;
using Data.Serialization;
using UnityEngine;

namespace Core.Behaviors.Interaction
{
    public class DamageReceiver : IInternalEventReceiver, IDamageProvider
    {
        public event Action<int> OnTakeDamage;

        public void ReceiveEvent(IEvent @event)
        {
            if(@event is DamageData damageData)
            {
                Debug.Log($"Received damage: {damageData.Damage}");
                OnTakeDamage?.Invoke(damageData.Damage);
            }
        }
    }
}