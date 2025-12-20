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
        private void Construct(IInternalEventReceiverService internalEventReceiverService)
        {
            this.internalEventReceiverService = Extensions.AssignWithNullCheck(internalEventReceiverService);
        }
        public void ReceiveEvent(IEvent @event)
        {
            // Debug.Log($"ColliderExternalEventReceiver received event: {@event.GetType().Name}");
            internalEventReceiverService.ReceiveEvent(@event);
        }
    }
}