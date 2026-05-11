using UnityEngine;
using Zenject;
using Utils;

namespace Core.Behaviors.Interaction
{
    [DisallowMultipleComponent]
    public class ColliderExternalEventReceiver : MonoBehaviour, IExternalEventReceiver, Providers.IProvider
    {
        private IInternalEventReceiverService internalEventReceiverService;

        [Inject]
        public void Construct(IInternalEventReceiverService internalEventReceiverService)
        {
            this.internalEventReceiverService = Extensions.AssignWithNullCheck(internalEventReceiverService);
        }

        /// <summary>
        /// Перенаправляет полученное внешнее событие в локальный сервис получения внутренних событий.
        /// </summary>
        public void ReceiveEvent(IEvent @event)
        {
            // Debug.Log($"ColliderExternalEventReceiver received event: {@event.GetType().Name}");
            internalEventReceiverService.ReceiveEvent(@event);
        }
    }
}