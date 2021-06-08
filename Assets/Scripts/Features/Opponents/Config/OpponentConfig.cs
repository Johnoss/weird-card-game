using UnityEngine;
using Utilities.Ranged_Float;

namespace Features.Opponents.Config
{
    [CreateAssetMenu(fileName = "OpponentConfig", menuName = "Config/OpponentConfig")]
    public class OpponentConfig : ScriptableObject
    {
        [Header("Visuals")]
        [SerializeField] private string title;
        public string Title => title;
        public Color textColor;
        public Color TextColor => textColor;

        [Header("Balancing")]
        [RangedFloat(0, 3000, RangedFloatAttribute.RangeDisplayType.EditableRanges)]
        [SerializeField] private RangedFloat defaultScore;
        public RangedFloat DefaultScore => defaultScore;
        [RangedFloat(0, 3000)]
        [SerializeField] private RangedFloat scoreSoftLimit;
        public RangedFloat ScoreSoftLimit => scoreSoftLimit;
        [RangedFloat(-100, 100)]
        [Tooltip("In percent (50 => 50%)")]
        [SerializeField] private RangedFloat scoreChangePerTurnRange;
        public float ScoreChangePerTurnRange => scoreChangePerTurnRange.GetRandomValue() / 100f;

    }
}