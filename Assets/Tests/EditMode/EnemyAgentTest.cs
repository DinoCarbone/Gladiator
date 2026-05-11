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
            var agent = new EnemyAgent(selfTransform, AttackAngleThreshold, AttackDistance);

            agent.Construct(playerSceneProvider);

            Assert.AreEqual(targetTransform, agent.TargetTransform);
        }

        [Test]
        public void IsAttack_TargetWithinDistanceAndLookingAtPlayer_ReturnsTrue()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            Assert.IsTrue(agent.IsAttack);
        }

        [Test]
        public void IsAttack_TargetBeyondDistance_ReturnsFalse()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            Assert.IsFalse(agent.IsAttack);
        }

        [Test]
        public void IsAttack_TargetInRangeButNotFacingTarget_ReturnsFalse()
        {
            targetTransform.position = selfTransform.position + Vector3.right * 3f;
            var agent = new EnemyAgent(selfTransform, 5f, AttackDistance);
            agent.Construct(playerSceneProvider);

            Assert.IsFalse(agent.IsAttack);
        }

        [Test]
        public void IsAttack_TargetAtExactDistance_ReturnsTrue()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * AttackDistance;
            var agent = CreateAgent();

            Assert.IsTrue(agent.IsAttack);
        }

        [Test]
        public void IsHandle_TargetBeyondDistance_ReturnsTrue()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            Assert.IsTrue(agent.IsHandle);
        }

        [Test]
        public void IsHandle_TargetWithinDistance_ReturnsFalse()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            Assert.IsFalse(agent.IsHandle);
        }

        [Test]
        public void IsHandle_TargetAtExactDistance_ReturnsFalse()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * AttackDistance;
            var agent = CreateAgent();

            Assert.IsFalse(agent.IsHandle);
        }

        [Test]
        public void Axis_WhenIsHandle_ReturnsUp()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 10f;
            var agent = CreateAgent();

            Assert.AreEqual(Vector2.up, agent.Axis);
        }

        [Test]
        public void Axis_WhenNotIsHandle_ReturnsZero()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            Assert.AreEqual(Vector2.zero, agent.Axis);
        }

        [Test]
        public void Rotation_TargetInFront_ReturnsForwardRotation()
        {
            targetTransform.position = selfTransform.position + Vector3.forward * 3f;
            var agent = CreateAgent();

            var expectedRotation = Quaternion.LookRotation(Vector3.forward);
            var actualRotation = agent.Rotation;

            AssertQuaternionsApproximatelyEqual(expectedRotation, actualRotation);
        }

        [Test]
        public void Rotation_ZeroDirection_ReturnsCurrentRotation()
        {
            targetTransform.position = selfTransform.position;
            var agent = CreateAgent();

            var rotation = agent.Rotation;

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