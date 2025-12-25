using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "EventRouter",
      menuName = "ScriptableObjects/Providers/Interactions/EventRouter")]
    public class EventRouterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            /// <summary>Создаёт маршрутизатор событий (EventRouter) для взаимодействий.</summary>
            return new EventRouter();
        }
    }
}