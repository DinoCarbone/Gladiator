using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.ScriptableObjects.Animatios;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "DamageDealer",
      menuName = "ScriptableObjects/Providers/Interactions/DamageDealer")]
    public class DamageDealerSO : BaseProviderSO
    {
        [SerializeField, Tooltip("Effect radius for the damage dealer.")]
        private float radius;

        [SerializeField, Tooltip("Damage amount dealt by this dealer.")]
        private int damage;

        [SerializeField, Tooltip("Maximum distance at which this attack can hit.")]
        private float distance;

        [SerializeField, Tooltip("Animation event to trigger when dealing damage.")]
        private AnimationEventSO animationEvent;

        /// <summary>Создаёт провайдер `DamageDealer` с заданными параметрами.</summary>
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new DamageDealer(animationEvent, damage, distance, radius);
        }
    }
}