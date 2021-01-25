using Assets.Scripts.Features.Animations.Card.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Card
{
    [RequireComponent(typeof(RectTransform))]
    public class PlayCardTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private PlayCardTweenerConfig playCardTweenerConfig;

        private RectTransform rectTransform;
        private Transform defaultParent;

        private Transform selectedCardParent;

        private Tweener blendableTweener;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultParent = transform.parent;
        }

        public void SetupReferences(Transform selectedCardParent)
        {
            this.selectedCardParent = selectedCardParent;
        }

        public void Animate()
        {
            ResetTweeners();

            Tweener = rectTransform
                .DOAnchorPos(playCardTweenerConfig.TargetOffset, playCardTweenerConfig.AnimationDurationSeconds)
                .SetEase(playCardTweenerConfig.Ease);
            blendableTweener = rectTransform.DOBlendableLocalRotateBy(playCardTweenerConfig.TargetRotateBy, playCardTweenerConfig.AnimationDurationSeconds)
                .SetEase(playCardTweenerConfig.Ease);

        }

        private void ResetTweeners()
        {
            blendableTweener.Kill();
            KillTweener();
        }
    }
}
