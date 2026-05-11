using Core.Behaviors.Lifecycle;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class DeathRouterTests
    {
        [Test]
        public void RegisterDeath_PlayerKillableData_InvokesOnPlayerDied()
        {
            // Arrange
            var deathRouter = new DeathRouter();
            var playerKillableData = Substitute.For<IPlayerKillableData>();
            IPlayerKillableData receivedData = null;
            deathRouter.OnPlaerDied += (data) => receivedData = data;

            // Act
            deathRouter.RegisterDeath(playerKillableData);

            // Assert
            Assert.AreEqual(playerKillableData, receivedData);
        }

        [Test]
        public void RegisterDeath_EnemyKillableData_InvokesOnEnemyDied()
        {
            // Arrange
            var deathRouter = new DeathRouter();
            var enemyKillableData = Substitute.For<IEnemyKillableData>();
            IEnemyKillableData receivedData = null;
            deathRouter.OnEnemyDied += (data) => receivedData = data;

            // Act
            deathRouter.RegisterDeath(enemyKillableData);

            // Assert
            Assert.AreEqual(enemyKillableData, receivedData);
        }

        [Test]
        public void RegisterDeath_PlayerKillableData_DoesNotInvokeOnEnemyDied()
        {
            // Arrange
            var deathRouter = new DeathRouter();
            var playerKillableData = Substitute.For<IPlayerKillableData>();
            var enemyDiedCalled = false;
            deathRouter.OnEnemyDied += (data) => enemyDiedCalled = true;

            // Act
            deathRouter.RegisterDeath(playerKillableData);

            // Assert
            Assert.IsFalse(enemyDiedCalled);
        }

        [Test]
        public void RegisterDeath_EnemyKillableData_DoesNotInvokeOnPlayerDied()
        {
            // Arrange
            var deathRouter = new DeathRouter();
            var enemyKillableData = Substitute.For<IEnemyKillableData>();
            var playerDiedCalled = false;
            deathRouter.OnPlaerDied += (data) => playerDiedCalled = true;

            // Act
            deathRouter.RegisterDeath(enemyKillableData);

            // Assert
            Assert.IsFalse(playerDiedCalled);
        }
    }
}