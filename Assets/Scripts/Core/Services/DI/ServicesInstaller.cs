using Core.Providers;
using Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHybridInjectService>()
                 .To<HybridInjectService>()
                 .AsSingle();
                 
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            
            Container.Bind<ICameraProvider>()
                 .To<CameraProvider>().FromInstance(new CameraProvider(Camera.main))
                 .AsSingle();
        }
    }
}