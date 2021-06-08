using Features.Card;
using Features.Card.Config;
using Features.Card.Deck;
using Features.Card.Factory;
using Features.Card.Hand;
using Features.Card.SelectedCard;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    public class CardsInstaller : MonoInstaller
    {
        [SerializeField] private CardSlotsView cardSlotsView;

        [SerializeField] private DeckConfig deckConfig;

        public override void InstallBindings()
        {
            Container.Bind<CardFactory>().AsSingle();

            Container.BindInstance(cardSlotsView);

            Container
                .BindFactory<CardModel, CardController, SelectedCardModel, CardGesturesModel, DraggableController,
                    Transform, CardView, CardView.ViewFactory>()
                .FromComponentInNewPrefab(deckConfig.CardPrefab);

            InstallDeck();
            InstallSelectedCard();
        }

        private void InstallDeck()
        {
            Container.Bind<DeckModel>().AsSingle();
        }

        private void InstallSelectedCard()
        {
            Container.Bind<SelectedCardModel>().AsSingle();
            Container.Bind<SelectedCardController>().AsSingle();
        }
    }
}