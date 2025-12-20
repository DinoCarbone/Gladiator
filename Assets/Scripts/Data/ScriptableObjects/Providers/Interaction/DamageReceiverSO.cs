using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "DamageReceiver",
      menuName = "ScriptableObjects/Providers/Interactions/DamageReceiver")]
    public class DamageReceiverSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new DamageReceiver();
        }
    }
}