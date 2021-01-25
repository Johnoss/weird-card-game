using Assets.Scripts.Features.UI;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIModel>().AsSingle();
            Container.Bind<UIController>().AsSingle();
        }
    }
}