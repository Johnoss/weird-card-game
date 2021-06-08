using System.Collections.Generic;
using Features.Card.Effects;
using UnityEngine;

namespace Features.Card
{
    public class CardData
    {
        public Sprite IconSprite { get; }
        public string TitleText { get; }
        public string DescriptionText { get; }
        public List<CardEffect> CardEffects { get; }

        public CardData(Sprite iconSprite, string titleText, string descriptionText, List<CardEffect> cardEffects)
        {
            IconSprite = iconSprite;
            TitleText = titleText;
            DescriptionText = descriptionText;
            CardEffects = cardEffects;
        }
    }
}