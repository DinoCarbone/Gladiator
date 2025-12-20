using Data.ScriptableObjects.Animatios;

namespace Core.Behaviors.Interaction
{
    public interface IAnimationEventReceiveService
    {
        void OnAnimationEvent(AnimationEventSO animationEvent);
    }
    public interface IInternalEventReceiverService
    {
        void ReceiveEvent(IEvent @event);
    }
    public interface IInternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }
    public interface IAnimationEventReceiver
    {
        void ReceiveAnimationEvent(AnimationEventSO animationEvent);
    }
    public interface IEvent{}
    public interface IDamageEvent
    {
        int Damage { get; }
    }
    public interface IExternalEventEmitter
    {
        void EmitEvent(IEvent @event, float distance, float radius);
    }
    public interface IExternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }
}