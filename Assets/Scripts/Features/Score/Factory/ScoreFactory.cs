using System.Collections.Generic;
using Features.Card.SelectedCard;
using Features.Gauge;
using Features.Opponents.Config;
using Features.Score.Config;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Features.Score.Factory
{
    [UsedImplicitly]
    public class ScoreFactory
    {
            private const float Y_OFFSET = -30f;
        [Inject] private ScoreView.ViewFactory scoreViewFactory;

        [Inject] private ScoreConfig scoreConfig;
        [Inject] private CompetitionConfig competitionConfig;

        [Inject] private SelectedCardModel selectedCardModel;
        [Inject] private GameProgressModel gameProgressModel;

        [Inject] private GameSummaryView gameSummaryView;

        [Inject] private GameProgressController gameProgressController;

        public ScoreModel CreatePlayerScore(List<GaugeModel> gaugeModels)
        {
            var scoreModel = new ScoreModel(scoreConfig.PlayerName, scoreConfig.TextColor);
            var scoreController = new PlayerScoreController(scoreModel, selectedCardModel, gameProgressController,
                scoreConfig, gaugeModels);
            var scoreView = scoreViewFactory.Create(scoreModel);

            return scoreModel;
        }

        public List<ScoreModel> CreateOpponentScores()
        {
            var models = new List<ScoreModel>();
            var anchoredYOffset = Y_OFFSET;
            foreach (var opponent in competitionConfig.GetOpponents())
            {
                var scoreModel = new ScoreModel(opponent.Title, opponent.TextColor);
                var scoreController =
                    new MockScoreController(scoreModel, selectedCardModel, gameProgressModel, opponent);
                var scoreView = scoreViewFactory.Create(scoreModel);

                models.Add(scoreModel);

                scoreView.gameObject.GetComponent<RectTransform>().anchoredPosition += Vector2.up * anchoredYOffset;
                anchoredYOffset -= Y_OFFSET; //TODO proper positioning
            }
            return models;
        }

        public void SetupGameSummary()
        {
            gameSummaryView.Setup(gameProgressModel);
        }
    }
}
