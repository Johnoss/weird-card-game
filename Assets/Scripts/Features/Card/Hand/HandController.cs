using Assets.Scripts.Features.Card.Deck;
using Assets.Scripts.Features.Card.SelectedCard;
using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;
using Assets.Scripts.Features.Gauge;
using UniRx;

namespace Assets.Scripts.Features.Card.Hand
{
    [UsedImplicitly]
    public class HandController : AbstractController
    {
        private readonly HandModel handModel;
        private readonly DeckModel deckModel;

        private readonly SelectedCardModel selectedCardModel;
        private readonly SelectedCardController selectedCardController;
        private readonly GaugesContainerController gaugesContainerController;

        private CardModel SelectedCardModel =>
            handModel.HandCardBundles[selectedCardModel.SelectedCardIndex.Value].Model;

        public HandController(HandModel handModel, DeckModel deckModel, SelectedCardModel selectedCardModel,
            SelectedCardController selectedCardController, GaugesContainerController gaugesContainerController)
        {
            this.handModel = handModel;
            this.deckModel = deckModel;
            this.selectedCardModel = selectedCardModel;
            this.selectedCardController = selectedCardController;
            this.gaugesContainerController = gaugesContainerController;

            deckModel.CardsDeck.ObserveCountChanged()
                .Where(count => count <= 0)
                .Subscribe(_ => deckModel.ReshuffleDeck());

            selectedCardModel.OnPlaySelectedCard
                .Where(_ => selectedCardModel.IsCardSelected.Value)
                .Subscribe(_ => PlaySelectedCard());

            RedrawHand();
        }

        private void PlaySelectedCard()
        {
            var effects = SelectedCardModel.CardEffects;
            gaugesContainerController.ApplyEffects(effects);

            RedrawHand();
        }

        private void RedrawHand()
        {
            selectedCardController.DeselectCard();
            for (var i = 0; i < handModel.HandCardBundles.Count; i++)
            {
                var drawnCard = deckModel.DrawAndDiscard();
                handModel.SetCard(i, drawnCard);
            }
        }
    }
}