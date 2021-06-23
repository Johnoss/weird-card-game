using Features.Audio;
using Features.Card.SelectedCard;
using Features.MVC;
using UniRx;
using UnityEngine;

namespace Features.Card
{
    public class CardController : AbstractController
    {
        private readonly CardModel cardModel;
        private readonly SelectedCardController selectedCardController;
        private readonly AudioController audioController;

        public CardController(CardModel cardModel, SelectedCardController selectedCardController,
            AudioController audioController, SelectedCardModel selectedCardModel)
        {
            this.cardModel = cardModel;
            this.selectedCardController = selectedCardController;
            this.audioController = audioController;

            selectedCardModel.SelectedCardIndex
                .Select(selectedIndex => selectedIndex == cardModel.CardIndex)
                .Subscribe(cardModel.SetIsSelected);
        }

        public void RepopulateCard(CardData card)
        {
            cardModel.SetCardConfig(card);
        }

        public void SelectCard()
        {
            selectedCardController.SetSelectedCard(cardModel.CardIndex);
        }

        public void PlayCard()
        {
            cardModel.FireOnCardPlayed();
            selectedCardController.PlaySelectedCard();
        }
    }
}