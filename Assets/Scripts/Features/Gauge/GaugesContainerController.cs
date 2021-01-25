using System.Collections.Generic;
using Assets.Scripts.Features.Card.Effects;
using Assets.Scripts.Features.MVC;

namespace Assets.Scripts.Features.Gauge
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