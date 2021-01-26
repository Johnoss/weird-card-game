using Assets.Scripts.Features.Card;
using Assets.Scripts.Features.Card.Config;
using Assets.Scripts.Features.Card.Deck;
using Assets.Scripts.Features.Card.Factory;
using Assets.Scripts.Features.Card.Hand;
using Assets.Scripts.Features.Card.SelectedCard;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
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
                .BindFactory<CardModel, CardController, SelectedCardModel, Transform, CardView, CardView.ViewFactory>()
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