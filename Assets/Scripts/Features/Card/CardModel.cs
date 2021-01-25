using System.Collections.Generic;
using Assets.Scripts.Features.Card.Effects;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;
using UniRx;

namespace Assets.Scripts.Features.Card
{
    public class CardModel : AbstractModel
    {
        private readonly IReactiveProperty<CardData> cardData = new ReactiveProperty<CardData>();
        public IReadOnlyReactiveProperty<CardData> CardData => cardData;

        private readonly IReactiveProperty<bool> isSelected;
        public IReadOnlyReactiveProperty<bool> IsSelected => isSelected;

        private readonly IReactiveProperty<float> suspendedSeconds;
        public IReadOnlyReactiveProperty<float> SuspendedSeconds => suspendedSeconds;

        public IReadOnlyReactiveProperty<bool> IsSuspended => SuspendedSeconds.Select(value => value > 0).ToReadOnlyReactiveProperty();

        public Subject<Unit> OnCardPlayed { get; }

        private readonly CommonStatsConfig commonStatsConfig;

        public readonly int CardIndex;

        public List<CardEffect> CardEffects => cardData.Value.CardEffects;



        public CardModel(int cardIndex, CommonStatsConfig commonStatsConfig)
        {
            CardIndex = cardIndex;
            this.commonStatsConfig = commonStatsConfig;

            isSelected = new ReactiveProperty<bool>();
            suspendedSeconds = new ReactiveProperty<float>();
            OnCardPlayed = new Subject<Unit>();
        }

        public void SetIsSelected(bool selected)
        {
            isSelected.Value = selected;
        }

        public void SetSuspendCountDown(float suspendedSeconds)
        {
            this.suspendedSeconds.Value = suspendedSeconds;
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