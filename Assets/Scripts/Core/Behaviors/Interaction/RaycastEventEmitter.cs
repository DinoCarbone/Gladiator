using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Interaction
{
    public class RaycastEventEmitter : IExternalEventEmitter, IProvider
    {
        private readonly Transform source;

        public RaycastEventEmitter(Transform sourceTransform)
        {
            source = sourceTransform;
        }

        public void EmitEvent(IEvent @event, float distance, float radius)
        {
            Ray ray = new Ray(source.position, source.forward);

            if (Physics.SphereCast(ray, radius, out RaycastHit hit, distance))
            {
                hit.collider.GetComponent<IExternalEventReceiver>()?.ReceiveEvent(@event);
            }
        }
    }
}