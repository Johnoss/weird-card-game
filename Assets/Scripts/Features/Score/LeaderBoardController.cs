using System.Collections.Generic;
using System.Linq;
using Features.MVC;
using UniRx;

namespace Features.Score
{
    public class LeaderBoardController : AbstractController
    {
        private readonly List<ScoreModel> scoreModels;
        private readonly GameProgressModel gameProgressModel;

        public LeaderBoardController(List<ScoreModel> scoreModels, GameProgressModel gameProgressModel)
        {
            this.scoreModels = scoreModels;
            this.gameProgressModel = gameProgressModel;

            foreach (var scoreModel in scoreModels)
            {
                scoreModel.Score.Subscribe(_ => UpdateOrder());
            }

            gameProgressModel.HasPlayerWon
                .Where(hasWon => hasWon)
                .Subscribe(_ => OnPlayerWon());
        }

        private void OnPlayerWon()
        {
            //TODO animate and handle displaying of post game
        }

        private void UpdateOrder()
        {
            var orderedScoreModels = scoreModels.OrderByDescending(model => model.Score.Value).ToList();
            for (var i = 1; i <= orderedScoreModels.Count; i++)
            {
                orderedScoreModels[i - 1].SetOrder(i);
            }
        }
    }
}