using Features.SceneControl;
using Features.SessionSettings;
using Zenject;

namespace Installers.Persistent
{
    public class SceneControlInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneModel>().AsSingle();
            Container.Bind<SceneController>().AsSingle();
            Container.Bind<GameModeModel>().AsSingle();
            Container.Bind<GameModeController>().AsSingle();
        }
    }
}