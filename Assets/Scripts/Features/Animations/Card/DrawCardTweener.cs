using Assets.Scripts.Features.Animations.Card.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Card
{
    [RequireComponent(typeof(RectTransform))]
    public class DrawCardTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private DrawCardTweenerConfig tweenerConfig;

        private RectTransform rectTransform;
        private Vector2 defaultAnchoredPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultAnchoredPosition = rectTransform.anchoredPosition;
        }

        public void Animate(int index)
        {
            ResetTweeners();

            var delaySeconds = tweenerConfig.GetDelaySecondsForIndex(index);

            rectTransform.anchoredPosition = tweenerConfig.StartingAnchoredPosition;

            Tweener = rectTransform
                .DOAnchorPos(defaultAnchoredPosition, tweenerConfig.AnimationDurationSeconds)
                .SetEase(tweenerConfig.Ease)
                .SetDelay(delaySeconds);
        }

        private void ResetTweeners()
        {
            rectTransform.anchoredPosition = defaultAnchoredPosition;

            KillTweener();
        }
    }
}
