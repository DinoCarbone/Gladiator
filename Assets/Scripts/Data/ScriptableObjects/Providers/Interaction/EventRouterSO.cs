using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "EventRouter",
      menuName = "ScriptableObjects/Providers/Interactions/EventRouter")]
    public class EventRouterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new EventRouter();
        }
    }
}