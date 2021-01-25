using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Features.Actor
{
    public class ActorModel : AbstractModel
    {
        private const float MIN_POINTER_VALUE = 0;
        private const float MAX_POINTER_VALUE = 1;

        private readonly CommonStatsConfig commonStatsConfig;

        private readonly IReactiveProperty<float> gaugeValue;
        private readonly IReactiveProperty<float> scoreModifier;
        public IReadOnlyReactiveProperty<float> GaugeValue => gaugeValue;
        public IReadOnlyReactiveProperty<float> ScoreModifier => scoreModifier;

        public ActorModel(CommonStatsConfig commonStatsConfig)
        {
            this.commonStatsConfig = commonStatsConfig;
            gaugeValue = new ReactiveProperty<float>(commonStatsConfig.DefaultStatLevel.GetRandomValue());
            scoreModifier = new ReactiveProperty<float>();
        }

        public void UpdateGaugeValue(float deltaValue)
        {
            var newValue = gaugeValue.Value + deltaValue;
            newValue = Mathf.Clamp(newValue, MIN_POINTER_VALUE, MAX_POINTER_VALUE);

            gaugeValue.Value = newValue;
            scoreModifier.Value = commonStatsConfig.GetScoreSettingForValue(newValue).ScoreModifier;
        }
    }
}