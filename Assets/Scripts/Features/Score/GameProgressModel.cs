using Features.MVC;
using JetBrains.Annotations;
using UniRx;

namespace Features.Score
{
    [UsedImplicitly]
    public class GameProgressModel : AbstractModel
    {
        private readonly IReactiveProperty<bool> hasPlayerWon;
        public IReadOnlyReactiveProperty<bool> HasPlayerWon => hasPlayerWon;

        public GameProgressModel()
        {
            hasPlayerWon = new ReactiveProperty<bool>();
        }

        public void SetGameWon(bool gameWon)
        {
            hasPlayerWon.Value = gameWon;
        }
    }
}
