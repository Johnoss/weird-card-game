using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;

namespace Assets.Scripts.Features.Card.SelectedCard
{
    [UsedImplicitly]
    public class SelectedCardController : AbstractController
    {
        private readonly SelectedCardModel selectedCardModel;

        public SelectedCardController(SelectedCardModel selectedCardModel)
        {
            this.selectedCardModel = selectedCardModel;
        }

        public void DeselectCard()
        {
            selectedCardModel.DeselectCard();
        }

        public void SetSelectedCard(int cardIndex)
        {
            selectedCardModel.SetSelectedCard(cardIndex);
        }

        public void PlaySelectedCard()
        {
            selectedCardModel.PlaySelectedCard();
        }
    }
}