using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Interaction
{
    /// <summary>
    /// Проецирует луч от источника и отправляет внешнее событие в первый найденный коллайдер.
    /// Использует SphereCast для учёта радиуса эмиттера.
    /// </summary>
    public class RaycastEventEmitter : IExternalEventEmitter, IProvider
    {
        private readonly Transform source;

        public RaycastEventEmitter(Transform sourceTransform)
        {
            source = sourceTransform;
        }

        /// <summary>Выпускает событие вперед от источника на указанную дистанцию и радиус.</summary>
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