using Assets.Scripts.Features.Card.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Features.Gauge.Config
{
    [CreateAssetMenu(fileName = "GaugesConfig", menuName = "Config/GaugesConfig")]
    public class GaugesConfig : ScriptableObject
    {
        [Serializable]
        public class ReactionSetting
        {
            public CardEffect Effect;
            public Sprite ReactionSprite;
        }

        [Serializable]
        public class GaugeSetting
        {
            public string Name;
            public Sprite IconSprite;
            public float OffsetX;
            public List<Sprite> Expressions;
            public List<ReactionSetting> ReactionSettings;
        }

        [Header("Prefabs")]
        [SerializeField] private GaugeView gaugePrefab;
        public GaugeView GaugePrefab => gaugePrefab;

        [Header("Gauges")]
        [SerializeField] private List<GaugeSetting> gaugeSettings;
        public List<GaugeSetting> GaugeSettings => gaugeSettings;

    }
}