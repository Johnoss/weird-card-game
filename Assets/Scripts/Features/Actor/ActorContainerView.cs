using System.Collections.Generic;
using Features.Gauge;
using Features.MVC;
using UnityEngine;

namespace Features.Actor
{
    public class ActorContainerView : AbstractView
    {
        [Header("References")]
        [SerializeField] private List<ActorView> actorViews;

        public void Setup(List<GaugeModel> gaugeModels)
        {
            for (var i = 0; i < gaugeModels.Count; i++)
            {
                actorViews[i].Setup(gaugeModels[i]);
            }
        }
    }
}