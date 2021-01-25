using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Features.Card.Effects;
using UnityEngine;

namespace Assets.Scripts.Features.Gauge.Config
{
    [CreateAssetMenu(fileName = "CommonStatsConfig", menuName = "Config/CommonStatsConfig")]
    public class CommonStatsConfig : ScriptableObject
    {
        [Serializable]
        public class RangeScoreSetting
        {
            [Tooltip("Score modifier for when a stat is within given range")]
            [Header("Visuals")]
            public Color Color;
            public string Tooltip;
            [Header("Range for modifier")]
            [RangedFloat(0, 1)]
            public RangedFloat Range;
            public float ScoreModifier;
            public bool ShouldShake;
        }

        [Serializable]
        public class EffectSetting
        {
            public CardEffect CardEffect;
            [Header("Balancing")]
            [RangedFloat(-1, 1)]
            public RangedFloat EffectValue;
            [Header("Visuals")]
            public string EffectDescription;
            public Color EffectTextColor;
        }

        [Header("Visuals")]
        [SerializeField] private Gradient glowGradient;

        [Header("Common Balancing")]
        [RangedFloat(0, 1)]
        [SerializeField] private RangedFloat defaultStatLevel;
        public RangedFloat DefaultStatLevel => defaultStatLevel;

        [Header("Effect Ranges")]
        [SerializeField] private List<RangeScoreSetting> rangeScoreModifiers;

        [Header("Effect Definition")]
        [SerializeField] private List<EffectSetting> effectSettings;


        public RangeScoreSetting GetScoreSettingForValue(float value)
        {
            return rangeScoreModifiers.First(setting => setting.Range.min <= value && setting.Range.max >= value);
        }
        public int GetIndexOfValue(float value)
        {
            return rangeScoreModifiers.FindIndex(setting => setting.Range.min <= value && setting.Range.max >= value);
        }

        public EffectSetting GetEffectSetting(CardEffect effect)
        {
            return effectSettings.First(setting => setting.CardEffect == effect);
        }

        public Color GetColorByValue(float value)
        {
            return glowGradient.Evaluate(value);
        }
    }
}