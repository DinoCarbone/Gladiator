using Core.Behaviors.Interaction;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class DamageDealerTest
    {
        private const int Damage = 25;
        private const float Distance = 10f;
        private const float Radius = 2f;

        private DamageDealer damageDealer;
        private IExternalEventEmitter externalEventEmitter;
        private AnimationEventSO animationEvent;

        [SetUp]
        public void SetUp()
        {
            animationEvent = Substitute.For<AnimationEventSO>();
            externalEventEmitter = Substitute.For<IExternalEventEmitter>();

            damageDealer = new DamageDealer(animationEvent, Damage, Distance, Radius);
            damageDealer.Construct(externalEventEmitter);
        }

        [Test]
        public void ReceiveAnimationEvent_MatchingAnimationEvent_EmitsCorrectDamageData()
        {
            // Act
            damageDealer.ReceiveAnimationEvent(animationEvent);

            // Assert
            externalEventEmitter.Received(1).EmitEvent(
                Arg.Is<DamageData>(d => d.Damage == Damage),
                Distance,
                Radius
            );
        }
    }
}