using Core.Services.Input;
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
        }
    }
}