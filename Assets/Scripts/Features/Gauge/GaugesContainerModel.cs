using System.Collections.Generic;
using Assets.Scripts.Features.MVC;
using UniRx;

namespace Assets.Scripts.Features.Gauge
{
    public class GaugesContainerModel : AbstractModel
    {
        public IReactiveCollection<MCBundle<GaugeModel, GaugeController>> GaugeBundles { get; }

        public GaugesContainerModel(IEnumerable<MCBundle<GaugeModel, GaugeController>> gaugeBundles)
        {
            GaugeBundles = gaugeBundles.ToReactiveCollection();
        }

        public GaugeController GetGaugeController(int index)
        {
            return GaugeBundles[index].Controller;
        }
    }
}