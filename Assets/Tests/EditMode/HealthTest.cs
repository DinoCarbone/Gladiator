using Core.Behaviors.Health;
using Core.Behaviors.Interaction;
using NUnit.Framework;
using Data.Serialization;
using NSubstitute;

namespace Tests.EditMode
{
    public class HealthTest
    {
        [Test]
        public void Constructor_MaxHealth100_HealthSetTo100()
        {
            // Arrange & Act
            var service = new HealthService(100);

            // Assert
            Assert.AreEqual(100, service.Health);
            Assert.AreEqual(100, service.MaxHealth);
        }

        [Test]
        public void ReceiveEvent_Damage30_HealthReducedTo70()
        {
            // Arrange
            var service = new HealthService(100);

            // Act
            service.ReceiveEvent(new DamageData(30));

            // Assert
            Assert.AreEqual(70, service.Health);
        }
        [Test]
        public void ReceiveEvent_Damage30_MaxHealthNotReduced()
        {
            // Arrange
            var service = new HealthService(100);

            // Act
            service.ReceiveEvent(new DamageData(30));

            // Assert
            Assert.AreEqual(100, service.MaxHealth);
        }

        [Test]
        public void ReceiveEvent_LethalDamage_HealthClampedToZero()
        {
            // Arrange
            var service = new HealthService(100);

            // Act
            service.ReceiveEvent(new DamageData(150));

            // Assert
            Assert.AreEqual(0, service.Health);
        }

        [Test]
        public void ReceiveEvent_ZeroDamage_HealthUnchanged()
        {
            // Arrange
            var service = new HealthService(100);

            // Act
            service.ReceiveEvent(new DamageData(0));

            // Assert
            Assert.AreEqual(100, service.Health);
        }

        [Test]
        public void ReceiveEvent_OtherEventType_HealthUnchanged()
        {
            // Arrange
            var service = new HealthService(100);

            // Act
            var healthSubstitute = Substitute.For<IEvent>();
            service.ReceiveEvent(healthSubstitute);

            // Assert
            Assert.AreEqual(100, service.Health);
        }

        [Test]
        public void OnTakeDamage_Damage30_InvokedWith30()
        {
            // Arrange
            var service = new HealthService(100);
            int actualDamage = 0;
            service.OnTakeDamage += d => actualDamage = d;

            // Act
            service.ReceiveEvent(new DamageData(30));

            // Assert
            Assert.AreEqual(30, actualDamage);
        }

        [Test]
        public void OnTakeDamage_ZeroDamage_NotInvoked()
        {
            // Arrange
            var service = new HealthService(100);
            bool invoked = false;
            service.OnTakeDamage += _ => invoked = true;

            // Act
            service.ReceiveEvent(new DamageData(0));

            // Assert
            Assert.IsFalse(invoked);
        }

        [Test]
        public void OnChangeHealth_Damage30_InvokedWith70()
        {
            // Arrange
            var service = new HealthService(100);
            int actualHealth = 0;
            service.OnChangeHealth += h => actualHealth = h;

            // Act
            service.ReceiveEvent(new DamageData(30));

            // Assert
            Assert.AreEqual(70, actualHealth);
        }

        [Test]
        public void OnDie_LethalDamage_Invoked()
        {
            // Arrange
            var service = new HealthService(100);
            bool died = false;
            service.OnDie += () => died = true;

            // Act
            service.ReceiveEvent(new DamageData(150));

            // Assert
            Assert.IsTrue(died);
        }

        [Test]
        public void OnDie_NonLethalDamage_NotInvoked()
        {
            // Arrange
            var service = new HealthService(100);
            bool died = false;
            service.OnDie += () => died = true;

            // Act
            service.ReceiveEvent(new DamageData(50));

            // Assert
            Assert.IsFalse(died);
        }

        [Test]
        public void OnDie_TwoLethalHits_InvokedOnce()
        {
            // Arrange
            var service = new HealthService(100);
            int deathCount = 0;
            service.OnDie += () => deathCount++;

            // Act
            service.ReceiveEvent(new DamageData(110));
            service.ReceiveEvent(new DamageData(110));

            // Assert
            Assert.AreEqual(1, deathCount);
        }
    }
}