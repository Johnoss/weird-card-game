using Features.SceneControl;
using Zenject;

namespace Installers.Persistent
{
    public class SceneControlInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneModel>().AsSingle();
            Container.Bind<SceneController>().AsSingle();
        }
    }
}