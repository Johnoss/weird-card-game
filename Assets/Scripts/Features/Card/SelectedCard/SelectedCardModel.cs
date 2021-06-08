using Features.MVC;
using JetBrains.Annotations;
using UniRx;

namespace Features.Card.SelectedCard
{
    [UsedImplicitly]
    public class SelectedCardModel : AbstractModel
    {
        private const int NONE_SELECTED_INDEX = -1;

        private readonly ReactiveProperty<int> selectedCardIndex;
        public IReadOnlyReactiveProperty<int> SelectedCardIndex => selectedCardIndex;

        public readonly IReadOnlyReactiveProperty<bool> IsCardSelected;

        public Subject<Unit> OnPlaySelectedCard { get; }

        public SelectedCardModel()
        {
            selectedCardIndex = new ReactiveProperty<int>(NONE_SELECTED_INDEX);
            IsCardSelected = selectedCardIndex.Select(index => index != NONE_SELECTED_INDEX)
                .ToReadOnlyReactiveProperty();

            OnPlaySelectedCard = new Subject<Unit>();
        }

        public void SetSelectedCard(int cardIndex)
        {
            selectedCardIndex.Value = cardIndex;
        }

        public void DeselectCard()
        {
            selectedCardIndex.Value = NONE_SELECTED_INDEX;
        }

        public void PlaySelectedCard()
        {
            OnPlaySelectedCard.OnNext(Unit.Default);
        }
    }
}