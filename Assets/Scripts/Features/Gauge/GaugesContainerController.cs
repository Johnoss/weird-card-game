using System.Collections.Generic;
using Features.Card.Effects;
using Features.MVC;

namespace Features.Gauge
{
    public class GaugesContainerController : AbstractController
    {
        private readonly GaugesContainerModel gaugesContainerModel;

        public GaugesContainerController(GaugesContainerModel gaugesContainerModel)
        {
            this.gaugesContainerModel = gaugesContainerModel;
        }

        public void ApplyEffects(List<CardEffect> effectValues)
        {
            for (var i = 0; i < effectValues.Count; i++)
            {
                gaugesContainerModel.GetGaugeController(i).UpdatePointer(effectValues[i]);
            }
        }
    }
}