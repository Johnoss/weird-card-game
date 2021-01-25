using UnityEngine;

namespace Assets.Scripts.Features.Score.Config
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Config/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [Header("Prefabs")] [SerializeField] private ScoreView scorePrefab;
        public ScoreView ScorePrefab => scorePrefab;

        [Header("Visuals")]
        public string playerName;
        public string PlayerName => playerName;
        public Color textColor;
        public Color TextColor => textColor;

        [Header("Balancing")]
        [RangedFloat(0, 1000, RangedFloatAttribute.RangeDisplayType.EditableRanges)]
        [SerializeField] private RangedFloat defaultScore;
        public RangedFloat DefaultScore => defaultScore;
        [SerializeField] private float softMinScore;
        public float SoftMinScore => softMinScore;

    }
}