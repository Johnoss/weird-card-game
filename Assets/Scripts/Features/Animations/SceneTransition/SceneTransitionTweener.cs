using Assets.Scripts.Features.Animations.SceneTransition.Config;
using Assets.Scripts.Utilities.Extensions;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.SceneTransition
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private SceneTransitionTweenerConfig tweenerConfig;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Animate(bool fadeIn)
        {
            KillTweener();

            var endValue = fadeIn ? 1 : 0;
            var ease = fadeIn ? tweenerConfig.Ease : tweenerConfig.FadeOutEase;

            Tweener = canvasGroup
                .DOFade(endValue, tweenerConfig.AnimationDurationSeconds)
                .SetEase(ease);
        }
    }
}
