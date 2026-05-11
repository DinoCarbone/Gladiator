using Core.Behaviors.Interaction;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.EditMode
{
    public class EventRouterTests
    {
        [Test]
        public void OnAnimationEvent_ForwardsToAllAnimationReceivers()
        {
            // Arrange
            var receiver1 = Substitute.For<IAnimationEventReceiver>();
            var receiver2 = Substitute.For<IAnimationEventReceiver>();
            var eventRouter = CreateEventRouter(new List<object> { receiver1, receiver2 });
            var animationEvent = Substitute.For<AnimationEventSO>();

            // Act
            eventRouter.OnAnimationEvent(animationEvent);

            // Assert
            receiver1.Received(1).ReceiveAnimationEvent(animationEvent);
            receiver2.Received(1).ReceiveAnimationEvent(animationEvent);
        }

        [Test]
        public void ReceiveEvent_ForwardsToAllInternalReceivers()
        {
            // Arrange
            var receiver1 = Substitute.For<IInternalEventReceiver>();
            var receiver2 = Substitute.For<IInternalEventReceiver>();
            var eventRouter = CreateEventRouter(new List<object> { receiver1, receiver2 });
            var @event = Substitute.For<IEvent>();

            // Act
            eventRouter.ReceiveEvent(@event);

            // Assert
            receiver1.Received(1).ReceiveEvent(@event);
            receiver2.Received(1).ReceiveEvent(@event);
        }

        private EventRouter CreateEventRouter(List<object> entityDataList)
        {
            var eventRouter = new EventRouter();
            var allEntityData = new AllEntityData(entityDataList);
            eventRouter.Construct(allEntityData);
            return eventRouter;
        }
    }
}