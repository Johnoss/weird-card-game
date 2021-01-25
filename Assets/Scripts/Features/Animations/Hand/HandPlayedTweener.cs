using Assets.Scripts.Features.Animations.Hand.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Hand
{
    [RequireComponent(typeof(RectTransform))]
    public class HandPlayedTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private HandPlayedTweenerConfig handPlayedTweenerConfig;

        private RectTransform rectTransform;

        private Sequence hideSequence;
        private float defaultAnchoredY;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            hideSequence = DOTween.Sequence();

            defaultAnchoredY = rectTransform.anchoredPosition.y;
        }

        public void AnimatePlayed()
        {
            ResetAnimation();

            var segmentDurationSeconds = handPlayedTweenerConfig.AnimationDurationSeconds;

            hideSequence
                .Append(rectTransform
                    .DOAnchorPosY(handPlayedTweenerConfig.HideYPosition, segmentDurationSeconds)
                    .SetEase(handPlayedTweenerConfig.Ease));
            hideSequence
                .Append(rectTransform
                    .DOAnchorPosY(defaultAnchoredY, segmentDurationSeconds)
                    .SetDelay(segmentDurationSeconds)
                    .SetEase(handPlayedTweenerConfig.Ease));
        }

        private void ResetAnimation()
        {
            hideSequence?.Kill();
        }
    }
}
