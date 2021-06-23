using Features.MVC;
using JetBrains.Annotations;
using Zenject;

namespace Features.SessionSettings
{
    [UsedImplicitly]
    public class GameModeController : AbstractController
    {
        [Inject] private readonly GameModeModel gameModeModel;

        public void SetGameMode(GameMode gameMode)
        {
            gameModeModel.SetGameMode(gameMode);
        }
    }
}