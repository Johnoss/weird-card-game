using Features.Card.SelectedCard;
using Features.MVC;
using Features.Opponents.Config;
using UniRx;
using UnityEngine;
using Utilities.Extensions;

namespace Features.Score
{
    public class MockScoreController : AbstractController, IScoreController
    {
        private readonly IScoreModel scoreModel;
        private readonly GameProgressModel gameProgressModel;
        private readonly SelectedCardModel selectedCardModel;

        private readonly OpponentConfig opponentConfig;

        public MockScoreController(IScoreModel scoreModel, SelectedCardModel selectedCardModel,
            GameProgressModel gameProgressModel, OpponentConfig opponentConfig)
        {
            this.scoreModel = scoreModel;
            this.gameProgressModel = gameProgressModel;
            this.opponentConfig = opponentConfig;
            SetScore(opponentConfig.DefaultScore.GetRandomValue());

            gameProgressModel.HasPlayerWon.Subscribe(_ => UpdateShouldShow());

            selectedCardModel.OnPlaySelectedCard.Subscribe(_ => UpdateScore());
        }

        private void UpdateShouldShow()
        {
            scoreModel.SetShouldShow(!gameProgressModel.HasPlayerWon.Value);
        }

        public void SetScore(float score)
        {
            scoreModel.SetScore(score);
        }

        public void UpdateScore()
        {
            scoreModel.UpdateScore(GetScoreDelta());
        }

        public float GetScoreDelta()
        {
            var percentFactor = opponentConfig.ScoreChangePerTurnRange;
            var currentScore = scoreModel.Score.Value;
            var scoreDelta = currentScore * percentFactor;

            var isWithinRange =
                currentScore.IsWithinRange(opponentConfig.ScoreSoftLimit.min, opponentConfig.ScoreSoftLimit.max);

            //if within soft cap range, return standard formula
            if (isWithinRange)
            {
                return scoreDelta;
            }

            //if outside the bounds of soft range update score in the direction towards the range
            return currentScore.IsGreaterThan(opponentConfig.ScoreSoftLimit.max)
                ? -Mathf.Abs(scoreDelta)
                : Mathf.Abs(scoreDelta);
        }
    }
}