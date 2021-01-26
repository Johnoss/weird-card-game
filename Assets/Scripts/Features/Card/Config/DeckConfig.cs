using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Features.Card.Config
{
    [CreateAssetMenu(fileName = "DeckConfig", menuName = "Config/DeckConfig")]
    public class DeckConfig : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private string slotGameObjectName = "empty_slot_{0}";
        public CardView CardPrefab => cardPrefab;
        public string SlotGameObjectName => slotGameObjectName;

        [Header("Visuals")]
        [SerializeField] private List<Sprite> cardVariantsSprites;
        [RangedFloat(-45, 45)]
        [SerializeField] private RangedFloat handArcRotationRange;
        public List<Sprite> CardVariantsSprites => cardVariantsSprites;
        public RangedFloat HandArcRotationRange => handArcRotationRange;

        [Header("Game Rules")]
        [SerializeField] private int handSize;
        public int HandSize => handSize;

        [Header("Cards Deck")]
        [SerializeField] private CardPackConfig cardPack;
        public CardPackConfig CardPack => cardPack;

    }
}