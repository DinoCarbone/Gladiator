using Core.Behaviors.Animations;
using Core.Behaviors.Lifecycle;
using Core.Behaviors.UI;
using Core.Providers;
using Core.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Unity.Cinemachine;
using System;

namespace Core.Services.DI
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private Image playerHealthBar;
        [SerializeField] private PlayerSceneProvider playerPrefub;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject enemyPrefub;
        [SerializeField] private TextMeshProUGUI scoreDisplayText;

        public override void InstallBindings()
        {
            Container.Bind<IHybridInjectService>()
                 .To<HybridInjectService>()
                 .AsSingle();
                 
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            
            Container.Bind<ICameraProvider>()
                 .To<CameraProvider>().FromInstance(new CameraProvider(Camera.main))
                 .AsSingle();

            Container.BindInterfacesAndSelfTo<TickableService>().AsSingle();

            // Container.BindInterfacesTo<PlayerSceneProvider>().
            // FromComponentInNewPrefab(playerPrefub).AsSingle();

            Container.BindInterfacesTo<PlayerSceneProvider>().FromMethod(c => GetPlayerSceneProvider()).AsSingle();

            Container.Bind<IValueDisplay>().To<ImageValueDisplay>().AsSingle()
            .WithArguments(playerHealthBar).WhenInjectedInto<IHealthViewPlayerFactory>();

            Container.Bind<IHealthViewPlayerFactory>()
            .To<HealthViewPlayerFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<DeathRouter>().AsSingle();

            Container.Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle()
            .WithArguments(enemyPrefub, enemySpawnPoint, Container);

            Container.BindInterfacesAndSelfTo<ScoreTracker>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyLifeCycleTracker>().AsSingle();

            Container.BindInterfacesAndSelfTo<ScoreDisplay>().AsSingle().WithArguments(scoreDisplayText).NonLazy();

            Container.Bind<GameOverHandler>().AsSingle().NonLazy();

            BindAnimationFactories();

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

            cinemachineCamera.Follow = player.PointToLoockCamera;

            return player;
        }

        private void BindAnimationFactories()
        {
            Container.BindInterfacesAndSelfTo<AnimationPlayServiceFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationEndNotifierFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationEventsNotifierFactory>().AsSingle();
        }

    }
}