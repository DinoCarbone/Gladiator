using Core.Behaviors.Health;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class HealthTest
    {
        [Test]
        public void HealthService_DefaultValue_IsOneHundred()
        {
            var healthService = new HealthService(100);
            Assert.That(healthService.MaxHealth, Is.EqualTo(100));
        }
    }
}