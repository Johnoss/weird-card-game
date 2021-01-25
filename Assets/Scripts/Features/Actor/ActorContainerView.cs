using Assets.Scripts.Features.MVC;
using System.Collections.Generic;
using Assets.Scripts.Features.Gauge;
using UnityEngine;

namespace Assets.Scripts.Features.Actor
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