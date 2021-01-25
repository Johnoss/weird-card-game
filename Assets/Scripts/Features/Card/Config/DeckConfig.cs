using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Features.Card.Config
{
    [CreateAssetMenu(fileName = "DeckConfig", menuName = "Config/DeckConfig")]
    public class DeckConfig : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] private CardView cardPrefab;
        public CardView CardPrefab => cardPrefab;
        [SerializeField] private string slotGameObjectName = "empty_slot_{0}";
        public string SlotGameObjectName => slotGameObjectName;

        [Header("Visuals")]
        [SerializeField] private List<Sprite> cardVariantsSprites;
        public List<Sprite> CardVariantsSprites => cardVariantsSprites;
        [RangedFloat(-45,45)]
        [SerializeField] private RangedFloat handArcRotationRange;
        public RangedFloat HandArcRotationRange => handArcRotationRange;

        [Header("Game Rules")]
        [SerializeField] private int handSize;
        public int HandSize => handSize;

        [Header("Cards Deck")]
        [SerializeField] private CardPackConfig cardPack;
        public CardPackConfig CardPack => cardPack;
    }
}