using System;
using System.Collections.Generic;
using Assets.Scripts.Features.Card.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Features.Card.Config
{
    [CreateAssetMenu(fileName = "CardPackConfig", menuName = "Config/CardPackConfig")]
    public class CardPackConfig : ScriptableObject
    {
        private const int STATS_AMOUNT = 3;

        [Serializable]
        public class CardSetting
        {
            [Header("Card Properties")]
            public Sprite IconSprite;
            [Space(10)]
            public string TitleText;
            [TextArea]
            public string DescriptionText;
            public CardEffect[] CardEffects = new CardEffect[STATS_AMOUNT];
        }

        [Header("Cards Deck")]
        [ListDrawerSettings(AddCopiesLastElement = true, NumberOfItemsPerPage = 3)]
        [SerializeField] private List<CardSetting> cards;
        public List<CardSetting> Cards => cards;
    }
}