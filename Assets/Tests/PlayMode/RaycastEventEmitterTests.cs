using System.Collections;
using Core.Behaviors.Interaction;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class RaycastEventEmitterTests
    {
        private const float SphereCastDistance = 10f;
        private const float SphereCastRadius = 1f;
        private const float TargetForwardOffset = 5f;
        private const float TargetRightOffset = 0.5f;

        private Transform source;
        private GameObject target;
        private MockExternalEventReceiver receiver;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Arrange
            source = new GameObject("Source").transform;
            source.position = Vector3.zero;
            source.rotation = Quaternion.identity;

            target = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // сделал отступ 0.5, чтобы проверить радиус
            target.transform.position = source.position + Vector3.forward * TargetForwardOffset +
                                        Vector3.right * TargetRightOffset;
            receiver = target.AddComponent<MockExternalEventReceiver>();

            yield return new WaitForFixedUpdate();
        }

        [UnityTest]
        public IEnumerator EmitEvent_HitReceiver_ForwardsEvent()
        {
            // Arrange
            var emitter = new RaycastEventEmitter(source);
            var @event = Substitute.For<IEvent>();

            // Act
            emitter.EmitEvent(@event, SphereCastDistance, SphereCastRadius);

            // Assert
            Assert.IsTrue(receiver.ReceivedEvent);
            Assert.AreEqual(@event, receiver.Event);

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(source.gameObject);
            Object.DestroyImmediate(target);

            yield return null;
        }

        private class MockExternalEventReceiver : MonoBehaviour, IExternalEventReceiver
        {
            public bool ReceivedEvent { get; private set; }
            public IEvent Event { get; private set; }

            public void ReceiveEvent(IEvent @event)
            {
                ReceivedEvent = true;
                Event = @event;
            }
        }
    }
}