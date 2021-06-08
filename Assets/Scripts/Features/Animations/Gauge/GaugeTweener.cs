using DG.Tweening;
using Features.Animations.Gauge.Config;
using Features.Gauge.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Animations.Gauge
{
    [RequireComponent(typeof(RectTransform))]
    public class GaugeTweener : AbstractTweener
    {
        [Header("Components")]
        [SerializeField] private Slider slider;
        [SerializeField] private RectTransform gaugeTransform;
        [SerializeField] private Image glowImage;

        [Header("Configs")]
        [SerializeField] private GaugeTweenerConfig gaugeTweenerConfig;
        [SerializeField] private CommonStatsConfig commonStatsConfig;

        private Tweener gaugeTweener;
        private Tweener colorTweener;

        public void Animate(float targetValue)
        {
            ResetAnimation();

            Tweener = slider.DOValue(targetValue, gaugeTweenerConfig.AnimationDurationSeconds).SetEase(gaugeTweenerConfig.Ease);
            gaugeTweener = gaugeTransform
                .DOShakeScale(gaugeTweenerConfig.AnimationDurationSeconds, gaugeTweenerConfig.ShakeStrength)
                .SetEase(gaugeTweenerConfig.Ease);

            colorTweener = glowImage
                .DOBlendableColor(commonStatsConfig.GetColorByValue(targetValue), gaugeTweenerConfig.AnimationDurationSeconds)
                .SetEase(gaugeTweenerConfig.Ease);

        }

        private void ResetAnimation()
        {
            colorTweener?.Kill();
            gaugeTweener?.Kill();
            KillTweener();

            gaugeTransform.localScale = gaugeTweenerConfig.DefaultScale;
        }
    }
}
