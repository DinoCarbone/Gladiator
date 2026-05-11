using Core.Behaviors.Health;
using Core.Behaviors.Interaction;
using Data.Serialization;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class HealthTest
    {
        private const int DefaultMaxHealth = 100;
        private HealthService service;

        [SetUp]
        public void SetUp()
        {
            service = new HealthService(DefaultMaxHealth);
        }

        [Test]
        public void Constructor_MaxHealthSet_HealthEqualsMaxHealth()
        {
            Assert.AreEqual(DefaultMaxHealth, service.Health);
            Assert.AreEqual(DefaultMaxHealth, service.MaxHealth);
        }

        [Test]
        public void ReceiveEvent_ValidDamage_HealthReduced()
        {
            service.ReceiveEvent(new DamageData(30));

            Assert.AreEqual(70, service.Health);
        }

        [Test]
        public void ReceiveEvent_ValidDamage_MaxHealthNotReduced()
        {
            service.ReceiveEvent(new DamageData(30));

            Assert.AreEqual(DefaultMaxHealth, service.MaxHealth);
        }

        [Test]
        public void ReceiveEvent_LethalDamage_HealthClampedToZero()
        {
            service.ReceiveEvent(new DamageData(150));

            Assert.AreEqual(0, service.Health);
        }

        [Test]
        public void ReceiveEvent_ZeroDamage_HealthUnchanged()
        {
            service.ReceiveEvent(new DamageData(0));

            Assert.AreEqual(DefaultMaxHealth, service.Health);
        }

        [Test]
        public void ReceiveEvent_OtherEventType_HealthUnchanged()
        {
            var healthSubstitute = Substitute.For<IEvent>();
            service.ReceiveEvent(healthSubstitute);

            Assert.AreEqual(DefaultMaxHealth, service.Health);
        }

        [Test]
        public void OnTakeDamage_ValidDamage_InvokedWithCorrectDamage()
        {
            int actualDamage = 0;
            service.OnTakeDamage += d => actualDamage = d;

            service.ReceiveEvent(new DamageData(30));

            Assert.AreEqual(30, actualDamage);
        }

        [Test]
        public void OnTakeDamage_ZeroDamage_NotInvoked()
        {
            bool invoked = false;
            service.OnTakeDamage += _ => invoked = true;

            service.ReceiveEvent(new DamageData(0));

            Assert.IsFalse(invoked);
        }

        [Test]
        public void OnChangeHealth_DamageApplied_InvokedWithNewHealth()
        {
            int actualHealth = 0;
            service.OnChangeHealth += h => actualHealth = h;

            service.ReceiveEvent(new DamageData(30));

            Assert.AreEqual(70, actualHealth);
        }

        [Test]
        public void OnDie_LethalDamage_Invoked()
        {
            bool died = false;
            service.OnDie += () => died = true;

            service.ReceiveEvent(new DamageData(150));

            Assert.IsTrue(died);
        }

        [Test]
        public void OnDie_NonLethalDamage_NotInvoked()
        {
            bool died = false;
            service.OnDie += () => died = true;

            service.ReceiveEvent(new DamageData(50));

            Assert.IsFalse(died);
        }

        [Test]
        public void OnDie_TwoLethalHits_InvokedOnce()
        {
            int deathCount = 0;
            service.OnDie += () => deathCount++;

            service.ReceiveEvent(new DamageData(110));
            service.ReceiveEvent(new DamageData(110));

            Assert.AreEqual(1, deathCount);
        }
    }
}