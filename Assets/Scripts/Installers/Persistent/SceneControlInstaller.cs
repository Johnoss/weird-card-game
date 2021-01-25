using Assets.Scripts.Features.SceneControl;
using Zenject;

namespace Assets.Scripts.Installers.Persistent
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