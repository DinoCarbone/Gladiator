using Core.Behaviors.Animations;
using Core.Behaviors.Lifecycle;
using Core.Behaviors.UI;
using Core.Providers;
using Core.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Services.DI
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private Image playerHealthBar;
        [SerializeField] private Transform playerTransform;
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

            Container.Bind<IPlayerSceneProvider>().
            To<PlayerSceneProvider>().FromMethod(GetPlayerSceneProvider).AsSingle();

            Container.Bind<IHealthViewPlayerFactory>()
            .To<HealthViewPlayerFactory>().AsSingle();

            Container.Bind<IValueDisplay>()
            .FromMethod(GetValueDisplay).AsSingle().WhenInjectedInto<IHealthViewPlayerFactory>();

            Container.Bind<IDeathService>().
            To<MockDeathService>().AsSingle();

            BindAnimationFactories();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void BindAnimationFactories()
        {
            Container.BindInterfacesAndSelfTo<AnimationPlayServiceFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationEndNotifierFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationEventsNotifierFactory>().AsSingle();
        }
        private PlayerSceneProvider GetPlayerSceneProvider()
        {
            return new PlayerSceneProvider(playerTransform);
        }
        private IValueDisplay GetValueDisplay()
        {
            return new ImageValueDisplay(playerHealthBar);
        }

    }
}