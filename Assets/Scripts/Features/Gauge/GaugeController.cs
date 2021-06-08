using Features.Card.Effects;
using Features.Gauge.Config;
using Features.MVC;

namespace Features.Gauge
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