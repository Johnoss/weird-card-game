using Features.MVC;
using JetBrains.Annotations;
using UniRx;

namespace Features.SessionSettings
{
    [UsedImplicitly]
    public class GameModeModel : AbstractModel
    {
        private readonly ReactiveProperty<GameMode> currentGameMode = new ReactiveProperty<GameMode>();
        public IReadOnlyReactiveProperty<GameMode> CurrentGameMode => currentGameMode;

        public void SetGameMode(GameMode gameMode)
        {
            currentGameMode.Value = gameMode;
        }
    }
}