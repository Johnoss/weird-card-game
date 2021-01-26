using Assets.Scripts.Features.Audio;
using Assets.Scripts.Features.Card.Deck;
using Assets.Scripts.Features.Card.Hand;
using Assets.Scripts.Features.Card.SelectedCard;
using Assets.Scripts.Features.Gauge;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.Features.Card.Factory
{
    [UsedImplicitly]
    public class CardFactory
    {
        [Inject] private CardView.ViewFactory cardViewFactory;

        [Inject] private CardSlotsView cardSlotsView;

        [Inject] private CommonStatsConfig commonStatsConfig;

        [Inject] private DeckModel deckModel;
        [Inject] private SelectedCardModel selectedCardModel;

        [Inject] private SelectedCardController selectedCardController;
        [Inject] private AudioController audioController;

        public List<MCBundle<CardModel, CardController>> CreateCards()
        {
            var cardsBundles = new List<MCBundle<CardModel, CardController>>();

            for (var i = 0; i < cardSlotsView.CardSlots.Count; i++)
            {
                var cardBundle = CreateCard(i);
                cardsBundles.Add(cardBundle);
            }

            return cardsBundles;
        }

        public void CreateHand(List<MCBundle<CardModel, CardController>> handCardBundles,
            GaugesContainerController gaugesContainerController)
        {
            var handModel = new HandModel(handCardBundles);
            var handController = new HandController(handModel, deckModel, selectedCardModel, selectedCardController,
                gaugesContainerController);
        }

        private MCBundle<CardModel, CardController> CreateCard(int slotIndex)
        {
            var cardParent = cardSlotsView.CardSlots[slotIndex];

            var model = new CardModel(slotIndex, commonStatsConfig);
            var controller = new CardController(model, selectedCardController, audioController, selectedCardModel);
            var view = cardViewFactory.Create(model, controller, selectedCardModel, cardSlotsView.SelectedCardParent);
            view.transform.SetParent(cardParent, false);

            var cardBundle = new MCBundle<CardModel, CardController>(model, controller);
            return cardBundle;
        }
    }
}
