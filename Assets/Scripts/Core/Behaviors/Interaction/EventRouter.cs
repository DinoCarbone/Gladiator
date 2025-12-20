using System.Collections.Generic;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using Utils;
using Zenject;

namespace Core.Behaviors.Interaction
{
    public class EventRouter : Providers.IProvider, IAnimationEventReceiveService, IInternalEventReceiverService
    {
        private List<IAnimationEventReceiver> animationEventReceivers;
        private List<IInternalEventReceiver> internalEventReceivers;

        [Inject]
        private void Construct(AllEntityData allEntityData)
        {
            AllEntityData entityData = Extensions.AssignWithNullCheck(allEntityData);

            AddAnimationEventReceiver(entityData);
            AddInternalEventReceiver(entityData);
        }
        private void AddAnimationEventReceiver(AllEntityData allEntityData)
        {
            animationEventReceivers = new List<IAnimationEventReceiver>();
            foreach (object animationEventReceiver in allEntityData.EntityData)
            {
                 if(animationEventReceiver is IAnimationEventReceiver receiver) 
                 animationEventReceivers.Add(receiver);
            }
        }
        private void AddInternalEventReceiver(AllEntityData allEntityData)
        {
            internalEventReceivers = new List<IInternalEventReceiver>();
            foreach (object internalEventReceiver in allEntityData.EntityData)
            {
                 if(internalEventReceiver is IInternalEventReceiver receiver) 
                 internalEventReceivers.Add(receiver);
            }
        }
        public void OnAnimationEvent(AnimationEventSO animationEvent)
        {
            foreach (IAnimationEventReceiver animationEventReceiver in animationEventReceivers)
            {
                animationEventReceiver.ReceiveAnimationEvent(animationEvent);
            }   
        }
        public void ReceiveEvent(IEvent @event)
        {
            foreach (IInternalEventReceiver internalEventReceiver in internalEventReceivers)
            {
                internalEventReceiver.ReceiveEvent(@event);
            }
        }
    }
}