using DG.Tweening;
using Features.Animations.Card.Config;
using UnityEngine;

namespace Features.Animations.Card
{
    [RequireComponent(typeof(RectTransform))]
    public class TranslateToParentTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private TranslateToParentTweenerConfig selectedCardTweenerConfig;

        private RectTransform rectTransform;

        private Tweener scaleTweener;
        private Tweener rotateTweener;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Animate()
        {
            if (rectTransform == null)
            {
                rectTransform = gameObject.GetComponent<RectTransform>();
            }

            ResetTweeners();

            var scaleBy = Vector3.one - rectTransform.localScale;

            Tweener = rectTransform
                .DOAnchorPos(Vector2.zero, selectedCardTweenerConfig.AnimationDurationSeconds)
                .SetEase(selectedCardTweenerConfig.Ease);

            scaleTweener = rectTransform
                .DOBlendableScaleBy(scaleBy, selectedCardTweenerConfig.AnimationDurationSeconds)
                .SetEase(selectedCardTweenerConfig.Ease);

            rotateTweener = rectTransform
                .DOBlendableLocalRotateBy(-rectTransform.localEulerAngles,
                    selectedCardTweenerConfig.AnimationDurationSeconds)
                .SetEase(selectedCardTweenerConfig.Ease);

        }

        public void ResetTweeners()
        {
            rotateTweener?.Kill();
            scaleTweener?.Kill();
            KillTweener();
        }
    }
}
