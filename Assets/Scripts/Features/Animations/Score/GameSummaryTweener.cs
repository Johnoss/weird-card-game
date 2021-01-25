using Assets.Scripts.Features.Animations.Card;
using Assets.Scripts.Features.Animations.Card.Config;
using Assets.Scripts.Features.Animations.Score.Config;
using Assets.Scripts.Features.Animations.UI;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Score
{
    [RequireComponent(typeof(RectTransform))]
    public class GameSummaryTweener : AbstractTweener
    {
        [Header("Components")]
        [SerializeField] private RectTransform panelTransform;

        [Header("Sequence")]
        [SerializeField] private PinTweener pinTweener;

        [Header("Configs")]
        [SerializeField] private TranslateToParentTweenerConfig tweenerConfig;

        private Tweener rotateTweener;

        public void Animate(bool show)
        {
            RestartAnimation();

            pinTweener.Animate(show);

            Tweener = panelTransform
                .DOAnchorPos(Vector2.zero, tweenerConfig.AnimationDurationSeconds)
                .SetEase(tweenerConfig.Ease);

            rotateTweener = panelTransform
                .DOBlendableRotateBy(-panelTransform.localEulerAngles, tweenerConfig.AnimationDurationSeconds)
                .SetEase(tweenerConfig.Ease);
        }

        private void RestartAnimation()
        {
            rotateTweener?.Kill();
            KillTweener();
        }
    }
}
