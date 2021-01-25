using Assets.Scripts.Features.Card.Config;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Scripts.Features.Card.Deck
{
    public class DeckModel : AbstractModel
    {
        private readonly List<CardPackConfig.CardSetting> defaultDeck;

        private readonly IReactiveCollection<CardData> cardsDeck;
        public IReadOnlyReactiveCollection<CardData> CardsDeck => cardsDeck;

        private readonly IReactiveCollection<CardData> drawnAndDiscardedDeck;
        public IReadOnlyReactiveCollection<CardData> DrawnAndDiscardedDeck => drawnAndDiscardedDeck;

        public DeckModel(DeckConfig deckConfig)
        {
            defaultDeck = deckConfig.CardPack.Cards.ToList();
            cardsDeck = new ReactiveCollection<CardData>();
            drawnAndDiscardedDeck = new ReactiveCollection<CardData>();

            ReshuffleDeck();
        }

        public CardData DrawAndDiscard()
        {
            var lastCardIndex = cardsDeck.Count - 1;

            var drawnCardData = cardsDeck[lastCardIndex];
            cardsDeck.RemoveAt(lastCardIndex);

            drawnAndDiscardedDeck.Add(drawnCardData);

            return drawnCardData;
        }

        public void ReshuffleDeck()
        {
            ResetDecks();

            var shuffledDeck = defaultDeck.ToList();
            shuffledDeck.Shuffle();

            foreach (var card in shuffledDeck)
            {
                var cardData = new CardData(card.IconSprite,
                    card.TitleText,
                    card.DescriptionText,
                    card.CardEffects.ToList());
                cardsDeck.Add(cardData);
            }
        }

        private void ResetDecks()
        {
            cardsDeck.Clear();
            drawnAndDiscardedDeck.Clear();
        }
    }
}