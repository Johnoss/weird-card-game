using Features.MVC;
using JetBrains.Annotations;

namespace Features.Score
{
    [UsedImplicitly]
    public class GameProgressController : AbstractController
    {
        private readonly GameProgressModel gameProgressModel;

        public GameProgressController(GameProgressModel gameProgressModel)
        {
            this.gameProgressModel = gameProgressModel;
        }

        public void SetGameWon(bool gameWon)
        {
            gameProgressModel.SetGameWon(gameWon);
        }
    }
}