using Core.Behaviors.Agents;
using Core.Providers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class EnemyAgentTests
    {
        private Transform selfTransform;
        private Transform targetTransform;
        private IPlayerSceneProvider playerSceneProvider;
        private const float AttackAngleThreshold = 45f;
        private const float AttackDistance = 5f;

        [SetUp]
        public void SetUp()
        {
            selfTransform = Create.Transform(Vector3.zero, Quaternion.identity);
            targetTransform = Create.Transform(Vector3.forward * 10f, Quaternion.identity);
            playerSceneProvider = Substitute.For<IPlayerSceneProvider>();
            playerSceneProvider.Transform.Returns(targetTransform);
        }

        [Test]
        public void Construct_ValidPlayerSceneProvider_SetsTargetTransform()
        {
            // Arrange
            var agent = CreateAgent();
            
            // Assert
            Assert.AreEqual(targetTransform, agent.TargetTransform);
        }

        [Test]
        public void IsAttack_TargetWithinDistanceAndLookingAtPlayer_ReturnsTrue()
        {
           // Arrange 
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            // Act
            var result = agent.IsAttack;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAttack_TargetBeyondDistance_ReturnsFalse()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            // Act
            var result = agent.IsAttack;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAttack_TargetInRangeButNotFacingTarget_ReturnsFalse()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.right * 3f;
            var agent = new EnemyAgent(selfTransform, 5f, AttackDistance);
            agent.Construct(playerSceneProvider);

            // Act
            var result = agent.IsAttack;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAttack_TargetAtExactDistance_ReturnsTrue()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * AttackDistance;
            var agent = CreateAgent();

            // Act
            var result = agent.IsAttack;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsHandle_TargetBeyondDistance_ReturnsTrue()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            // Act
            var result = agent.IsHandle;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsHandle_TargetWithinDistance_ReturnsFalse()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            // Act
            var result = agent.IsHandle;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsHandle_TargetAtExactDistance_ReturnsFalse()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * AttackDistance;
            var agent = CreateAgent();

            // Act
            var result = agent.IsHandle;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Axis_WhenIsHandle_ReturnsUp()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            // Act
            var axis = agent.Axis;

            // Assert
            Assert.AreEqual(Vector2.up, axis);
        }

        [Test]
        public void Axis_WhenNotIsHandle_ReturnsZero()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            // Act
            var axis = agent.Axis;

            // Assert
            Assert.AreEqual(Vector2.zero, axis);
        }

        [Test]
        public void Rotation_TargetInFront_ReturnsForwardRotation()
        {
            // Arrange
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();
            var expectedRotation = Quaternion.LookRotation(Vector3.forward);

            // Act
            var actualRotation = agent.Rotation;

            // Assert
            AssertQuaternionsApproximatelyEqual(expectedRotation, actualRotation);
        }

        [Test]
        public void Rotation_ZeroDirection_ReturnsCurrentRotation()
        {
            // Arrange
            targetTransform.position = selfTransform.position;
            var agent = CreateAgent();

            // Act
            var rotation = agent.Rotation;

            // Assert
            AssertQuaternionsApproximatelyEqual(selfTransform.rotation, rotation);
        }

        private EnemyAgent CreateAgent()
        {
            var agent = new EnemyAgent(selfTransform, AttackAngleThreshold, AttackDistance);
            agent.Construct(playerSceneProvider);
            return agent;
        }

        private void AssertQuaternionsApproximatelyEqual(Quaternion expected, Quaternion actual,
            float tolerance = 0.01f)
        {
            var angle = Quaternion.Angle(expected, actual);
            Assert.LessOrEqual(angle, tolerance,
                $"Expected rotation {expected}, but got {actual} (angle diff: {angle})");
        }

        [TearDown]
        public void TearDown()
        {
            if (selfTransform != null) Object.DestroyImmediate(selfTransform.gameObject);
            if (targetTransform != null) Object.DestroyImmediate(targetTransform.gameObject);
        }
    }
}