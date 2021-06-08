using Features.Initializers;
using Zenject;

namespace Installers.EditScene
{
    public class EditSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EditSceneInitializer>().AsSingle().NonLazy();
        }
    }
}