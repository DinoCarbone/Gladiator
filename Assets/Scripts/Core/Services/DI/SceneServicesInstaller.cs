using Core.Behaviors.Lifecycle;
using Core.Behaviors.UI;
using Core.Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Unity.Cinemachine;

namespace Core.Services.DI
{
    /// <summary>
    /// Сцено-зависимый инсталлер — бинды, привязанные к объектам и ресурсам текущей сцены.
    /// </summary>
    public class SceneServicesInstaller : MonoInstaller
    {
        [Header("Scene References")]
        [SerializeField, Tooltip("Cinemachine camera that should follow the player.")]
        private CinemachineCamera cinemachineCamera;

        [SerializeField, Tooltip("UI Image used to display player's health.")]
        private Image playerHealthBar;

        [SerializeField, Tooltip("Player prefab provider used to instantiate player on scene start.")]
        private PlayerSceneProvider playerPrefub;

        [SerializeField, Tooltip("Transform where enemies will be spawned.")]
        private Transform enemySpawnPoint;

        [SerializeField, Tooltip("Transform where player will be spawned.")]
        private Transform playerSpawnPoint;

        [SerializeField, Tooltip("Enemy prefab GameObject used by the enemy factory.")]
        private GameObject enemyPrefub;

        [SerializeField, Tooltip("Text element used to display the score.")]
        private TextMeshProUGUI scoreDisplayText;

        public override void InstallBindings()
        {
            BindPlayerProvider();
            BindValueDisplay();
            BindPlayerViewFactory();
            BindEnemyFactory();
            BindScoreDisplay();
            BindLifecycleAndGameOver();
            LockCursor();
        }

        private void BindPlayerProvider()
        {
            Container.BindInterfacesTo<PlayerSceneProvider>()
                .FromMethod(c => GetPlayerSceneProvider())
                .AsSingle();
        }

        private void BindValueDisplay()
        {
            Container.Bind<IValueDisplay>()
                .To<ImageValueDisplay>()
                .AsSingle()
                .WithArguments(playerHealthBar)
                .WhenInjectedInto<IHealthViewPlayerFactory>();
        }

        private void BindPlayerViewFactory()
        {
            Container.Bind<IHealthViewPlayerFactory>()
                .To<HealthViewPlayerFactory>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle()
                .WithArguments(enemyPrefub, enemySpawnPoint, Container);
        }

        private void BindScoreDisplay()
        {
            Container.BindInterfacesAndSelfTo<ScoreDisplay>()
                .AsSingle()
                .WithArguments(scoreDisplayText)
                .NonLazy();
        }

        private void BindLifecycleAndGameOver()
        {
            Container.BindInterfacesAndSelfTo<EnemyLifeCycleTracker>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathRouter>().AsSingle();
            Container.Bind<GameOverHandler>().AsSingle().NonLazy();
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private PlayerSceneProvider GetPlayerSceneProvider()
        {
            PlayerSceneProvider player = Container.InstantiatePrefabForComponent<PlayerSceneProvider>(
                playerPrefub,
                playerSpawnPoint.position,
                playerSpawnPoint.rotation,
                null
            );

            if (cinemachineCamera != null)
            {
                cinemachineCamera.Follow = player.PointToLoockCamera;
            }

            return player;
        }
    }
}
