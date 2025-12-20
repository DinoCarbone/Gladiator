using Data.ScriptableObjects.Animatios;
using Zenject;
using Utils;
using Data.Serialization;

namespace Core.Behaviors.Interaction
{
    public class DamageDealer : IAnimationEventReceiver, Providers.IProvider
    {
        private readonly float radius = 1;
        private readonly int damage = 1;
        private readonly float distance = 100;
        IExternalEventEmitter externalEventEmitter;
        private readonly AnimationEventSO animationEvent;
        public DamageDealer(AnimationEventSO animationEvent, int damage, float distance, float radius)
        {
            this.animationEvent = animationEvent;
            this.damage = damage;
            this.distance = distance;
            this.radius = radius;
        }
        [Inject]
        private void Construct(IExternalEventEmitter externalEventEmitter)
        {
            this.externalEventEmitter = Extensions.AssignWithNullCheck(externalEventEmitter);
        }
        public void ReceiveAnimationEvent(AnimationEventSO animationEvent)
        {
            if(this.animationEvent == animationEvent)
            {
                externalEventEmitter.EmitEvent(new DamageData(damage), distance, radius);
            }
        }
    }
}