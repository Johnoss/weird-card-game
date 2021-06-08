using Features.Animations.Config;
using UnityEngine;

namespace Features.Animations.Card.Config
{
    [CreateAssetMenu(fileName = "DrawCardTweenerConfig", menuName = "Tweener/DrawCardTweenerConfig")]
    public class DrawCardTweenerConfig : AbstractTweenerConfig
    {
        [Header("Draw Card")]
        [SerializeField] private Vector2 startingAnchoredPosition;
        [SerializeField] private float drawCardDelaySeconds;
        [SerializeField] private float defaultDelaySeconds;
        public Vector2 StartingAnchoredPosition => startingAnchoredPosition;

        public float GetDelaySecondsForIndex(int index)
        {
            return defaultDelaySeconds + index * drawCardDelaySeconds;
        }
    }
}