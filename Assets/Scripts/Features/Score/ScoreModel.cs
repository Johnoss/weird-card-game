using Features.MVC;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Features.Score
{
    [UsedImplicitly]
    public class ScoreModel : AbstractModel, IScoreModel
    {
        private readonly IReactiveProperty<float> score;
        public IReadOnlyReactiveProperty<float> Score => score;
        public IReadOnlyReactiveProperty<int> DisplayScore { get; }

        private readonly IReactiveProperty<int> order;
        public IReadOnlyReactiveProperty<int> Order => order;

        private readonly IReactiveProperty<bool> shouldShow;
        public IReadOnlyReactiveProperty<bool> ShouldShow => shouldShow;

        public string PlayerName { get; }
        public Color TextColor { get; }

        public ScoreModel(string playerName, Color textColor)
        {
            score = new ReactiveProperty<float>();
            order = new ReactiveProperty<int>();
            shouldShow = new ReactiveProperty<bool>(true);

            PlayerName = playerName;
            TextColor = textColor;

            DisplayScore = score.Select(scoreValue => (int) scoreValue).ToReactiveProperty();
        }

        public void SetOrder(int newOrder)
        {
            order.Value = newOrder;
        }

        public void SetScore(float scoreValue)
        {
            score.Value = scoreValue;
        }

        public void UpdateScore(float deltaScore)
        {
            score.Value += deltaScore;
        }

        public void SetShouldShow(bool value)
        {
            shouldShow.Value = value;
        }
    }
}