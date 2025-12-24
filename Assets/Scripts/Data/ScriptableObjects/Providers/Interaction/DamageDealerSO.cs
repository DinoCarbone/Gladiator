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
        [SerializeField] private float radius;
        [SerializeField] private int damage;
        [SerializeField] private float distance;
        [SerializeField] private AnimationEventSO animationEvent;

        /// <summary>Создаёт провайдер `DamageDealer` с заданными параметрами.</summary>
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new DamageDealer(animationEvent, damage, distance, radius);
        }
    }
}