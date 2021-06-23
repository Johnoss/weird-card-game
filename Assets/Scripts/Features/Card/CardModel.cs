using System.Collections.Generic;
using Features.Card.Effects;
using Features.MVC;
using UniRx;

namespace Features.Card
{
    public class CardModel : AbstractModel
    {
        private readonly IReactiveProperty<CardData> cardData = new ReactiveProperty<CardData>();
        public IReadOnlyReactiveProperty<CardData> CardData => cardData;

        private readonly IReactiveProperty<bool> isSelected;
        public IReadOnlyReactiveProperty<bool> IsSelected => isSelected;

        public Subject<Unit> OnCardPlayed { get; }

        public readonly int CardIndex;

        public List<CardEffect> CardEffects => cardData.Value.CardEffects;

        public CardModel(int cardIndex)
        {
            CardIndex = cardIndex;

            isSelected = new ReactiveProperty<bool>();
            OnCardPlayed = new Subject<Unit>();
        }

        public void SetIsSelected(bool selected)
        {
            isSelected.Value = selected;
        }

        public void SetCardConfig(CardData card)
        {
            cardData.Value = card;
        }

        public void FireOnCardPlayed()
        {
            OnCardPlayed.OnNext(Unit.Default);
        }
    }
}