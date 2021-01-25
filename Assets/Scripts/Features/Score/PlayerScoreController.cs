using Assets.Scripts.Features.Card.SelectedCard;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Features.Score.Config;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Features.Gauge;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Features.Score
{
    [UsedImplicitly]
    public class PlayerScoreController : AbstractController, IScoreController
    {
        private readonly ScoreModel scoreModel;
        private readonly GameProgressController gameProgressController;
        private readonly ScoreConfig scoreConfig;
        private readonly List<GaugeModel> gaugeModels;

        public PlayerScoreController(ScoreModel scoreModel, SelectedCardModel selectedCardModel,
            GameProgressController gameProgressController, ScoreConfig scoreConfig, List<GaugeModel> gaugeModels)
        {
            this.scoreModel = scoreModel;
            this.gameProgressController = gameProgressController;
            this.scoreConfig = scoreConfig;
            this.gaugeModels = gaugeModels;
            SetScore(scoreConfig.DefaultScore.GetRandomValue());

            selectedCardModel.OnPlaySelectedCard.Subscribe(_ => UpdateScore());
            scoreModel.Order
                .DelayFrame(1)
                .Subscribe(_ => SetGameWon());
        }

        private void SetGameWon()
        {
            var gameWon = scoreModel.Order.Value == 1;
            gameProgressController.SetGameWon(gameWon);
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
            var percentFactor = gaugeModels.Average(model => model.ScoreModifier.Value);
            Debug.Log($"Player Percent Factor is {percentFactor}");
            var currentScore = scoreModel.Score.Value;
            var scoreDelta = currentScore * percentFactor;


            //if score is below the soft cap, always change score in positive direction
            return currentScore > scoreConfig.SoftMinScore
                ? scoreDelta
                : Mathf.Abs(scoreDelta);
        }
    }
}