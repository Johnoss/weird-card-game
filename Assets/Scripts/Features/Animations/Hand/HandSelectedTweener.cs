using Assets.Scripts.Features.Animations.Hand.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Hand
{
    [RequireComponent(typeof(RectTransform))]
    public class HandSelectedTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private HandSelectedTweenerConfig handSelectedTweenerConfig;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void AnimateSelected(bool isCardSelected)
        {
            var targetY = isCardSelected
                ? handSelectedTweenerConfig.HiddenYPosition
                : handSelectedTweenerConfig.DefaultYPosition;
            AnimateSelected(targetY);
        }

        private void AnimateSelected(float targetAnchoredY)
        {
            KillTweener();

            Tweener = rectTransform.DOAnchorPosY(targetAnchoredY, handSelectedTweenerConfig.AnimationDurationSeconds)
                .SetEase(handSelectedTweenerConfig.Ease);
        }
    }
}
