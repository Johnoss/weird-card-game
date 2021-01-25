using Assets.Scripts.Features.Card.Effects;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;

namespace Assets.Scripts.Features.Gauge
{
    public class GaugeController : AbstractController
    {
        private readonly GaugeModel model;
        private readonly CommonStatsConfig commonStatsConfig;


        public GaugeController(GaugeModel model, CommonStatsConfig commonStatsConfig)
        {
            this.model = model;
            this.commonStatsConfig = commonStatsConfig;
        }

        public void UpdatePointer(CardEffect cardEffect)
        {
            var deltaValue = commonStatsConfig.GetEffectSetting(cardEffect).EffectValue.GetRandomValue();
            model.SetLatestCardEffect(cardEffect);
            model.UpdateGaugeValue(deltaValue);
        }
    }
}