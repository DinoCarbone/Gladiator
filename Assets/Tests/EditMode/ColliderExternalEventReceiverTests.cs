using Core.Behaviors.Interaction;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests.EditMode
{
    public class ColliderExternalEventReceiverTests
    {
        private ColliderExternalEventReceiver receiver;
        private IInternalEventReceiverService internalEventReceiverService;

        [SetUp]
        public void SetUp()
        {
            receiver = new GameObject().AddComponent<ColliderExternalEventReceiver>();
            internalEventReceiverService = Substitute.For<IInternalEventReceiverService>();
            receiver.Construct(internalEventReceiverService);
        }

        [Test]
        public void ReceiveEvent_CalledTwice_ForwardsBothToInternalService()
        {
            var event1 = Substitute.For<IEvent>();
            var event2 = Substitute.For<IEvent>();

            receiver.ReceiveEvent(event1);
            receiver.ReceiveEvent(event2);

            internalEventReceiverService.Received(1).ReceiveEvent(event1);
            internalEventReceiverService.Received(1).ReceiveEvent(event2);
        }

        [TearDown]
        public void TearDown()
        {
            if (receiver != null)
                Object.DestroyImmediate(receiver.gameObject);
        }
    }
}