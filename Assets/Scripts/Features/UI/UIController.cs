using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;

namespace Assets.Scripts.Features.UI
{
    [UsedImplicitly]
    public class UIController : AbstractController
    {
        private readonly UIModel uiModel;

        public UIController(UIModel uiModel)
        {
            this.uiModel = uiModel;
        }

        public void ToggleLeaderBoardPinned()
        {
            uiModel.SetLeaderBoardPinned(!uiModel.IsLeaderBoardPinned.Value);
        }

        public void SetLeaderBoardHoveredOver(bool hovered)
        {
            uiModel.SetLeaderBoardHoveredOver(hovered);
        }

        public void SetLeaderBoardFullScreen(bool isFullScreen)
        {
            uiModel.SetLeaderBoardFullScreen(isFullScreen);
        }
    }
}