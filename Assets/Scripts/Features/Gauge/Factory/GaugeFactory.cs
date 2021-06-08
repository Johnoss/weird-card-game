using System.Linq;
using Features.Gauge.Config;
using Features.MVC;
using JetBrains.Annotations;
using Zenject;

namespace Features.Gauge.Factory
{
    [UsedImplicitly]
    public class GaugeFactory
    {
        [Inject] private GaugesConfig gaugesConfig;
        [Inject] private CommonStatsConfig commonStatsConfig;

        [Inject] private GaugeView.ViewFactory gaugeViewFactory;

        public MCBundle<GaugesContainerModel, GaugesContainerController> CreateGaugesContainerBundle()
        {
            var gaugeBundles = gaugesConfig.GaugeSettings.Select(CreateGaugeBundle).ToList();

            var gaugesContainerModel = new GaugesContainerModel(gaugeBundles);
            var gaugesContainerController = new GaugesContainerController(gaugesContainerModel);

            return new MCBundle<GaugesContainerModel, GaugesContainerController>(gaugesContainerModel,
                gaugesContainerController);
        }
        
        private MCBundle<GaugeModel, GaugeController> CreateGaugeBundle(GaugesConfig.GaugeSetting gaugeSetting)
        {
            var model = new GaugeModel(commonStatsConfig);
            var controller = new GaugeController(model, commonStatsConfig);
            var view = gaugeViewFactory.Create(model, gaugeSetting);

            var gaugeBundle = new MCBundle<GaugeModel, GaugeController>(model, controller);
            return gaugeBundle;
        }
    }
}
