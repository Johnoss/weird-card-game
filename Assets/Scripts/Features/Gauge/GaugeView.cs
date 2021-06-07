using Assets.Scripts.Features.Animations.Gauge;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Gauge
{
    public class GaugeView : AbstractView
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<GaugeModel, GaugesConfig.GaugeSetting, GaugeView> { }

        [Header("Components")]
        [SerializeField] private Image iconImage;
        [SerializeField] private RectTransform dynamicParent;

        [Header("Animation")]
        [SerializeField] private GaugeTweener gaugeTweener;

        [Header("Config")]
        [SerializeField] private CommonStatsConfig commonStatsConfig;

        private GaugeModel gaugeModel;
        private GaugesConfig.GaugeSetting gaugeSetting;

        [Inject]
        public void Construct(GaugeModel gaugeModel,
            GaugesConfig.GaugeSetting gaugeSetting)
        {
            this.gaugeSetting = gaugeSetting;
            this.gaugeModel = gaugeModel;
            Setup();
        }

        private void Setup()
        {
            iconImage.sprite = gaugeSetting.IconSprite;

            dynamicParent.anchoredPosition += Vector2.right * gaugeSetting.OffsetX;

            gaugeModel.GaugeValue.Subscribe(_ => UpdatePointer()).AddTo(this);
        }

        private void UpdatePointer()
        {
            var targetValue = gaugeModel.GaugeValue.Value;

            gaugeTweener.Animate(targetValue);
        }
    }
}