using Assets.Scripts.Features.Initializers;
using Zenject;

namespace Assets.Scripts.Installers.EditScene
{
    public class EditSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EditSceneInitializer>().AsSingle().NonLazy();
        }
    }
}