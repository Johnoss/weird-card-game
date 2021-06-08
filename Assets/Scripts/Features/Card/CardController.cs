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

        private readonly CompositeDisposable suspendDisposer;

        public CardController(CardModel cardModel, SelectedCardController selectedCardController,
            AudioController audioController, SelectedCardModel selectedCardModel)
        {
            this.cardModel = cardModel;
            this.selectedCardController = selectedCardController;
            this.audioController = audioController;

            suspendDisposer = new CompositeDisposable();

            selectedCardModel.SelectedCardIndex
                .Select(selectedIndex => selectedIndex == cardModel.CardIndex)
                .Subscribe(cardModel.SetIsSelected);

            Observable.EveryUpdate()
                .Where(_ => cardModel.IsSuspended.Value)
                .Subscribe(_ => cardModel.SetSuspendCountDown(cardModel.SuspendedSeconds.Value - Time.deltaTime));
        }

        public void RepopulateCard(CardData card)
        {
            cardModel.SetCardConfig(card);
        }

        public void SelectCard()
        {
            selectedCardController.SetSelectedCard(cardModel.CardIndex);
        }

        public void SuspendCard(float seconds)
        {
            if (cardModel.SuspendedSeconds.Value < seconds)
            {
                cardModel.SetSuspendCountDown(seconds);
            }
        }

        public void PlayCard()
        {
            cardModel.FireOnCardPlayed();
            selectedCardController.PlaySelectedCard();
        }
    }
}