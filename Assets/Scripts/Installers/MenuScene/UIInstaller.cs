using Features.Card.SelectedCard;
using Zenject;

namespace Installers.MenuScene
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SelectedCardModel>().AsSingle();
            Container.Bind<SelectedCardController>().AsSingle();
        }
    }
}