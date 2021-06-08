using UniRx;
using UnityEngine;

namespace Features.Score
{
    public interface IScoreModel
    {
        IReadOnlyReactiveProperty<float> Score { get; }
        IReadOnlyReactiveProperty<bool> ShouldShow { get; }
        IReadOnlyReactiveProperty<int> DisplayScore { get; }
        string PlayerName { get; }
        Color TextColor { get; }
        void SetScore(float scoreValue);
        void UpdateScore(float deltaScore);
        void SetShouldShow(bool value);
    }
}