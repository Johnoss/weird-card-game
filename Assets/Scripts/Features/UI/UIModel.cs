using Assets.Scripts.Features.MVC;
using UniRx;

namespace Assets.Scripts.Features.UI
{
    public class UIModel : AbstractModel
    {
        private readonly IReactiveProperty<bool> isLeaderBoardHoveredOver;
        private readonly IReactiveProperty<bool> isLeaderBoardPinned;
        private readonly IReactiveProperty<bool> isLeaderBoardFullScreen;

        public IReadOnlyReactiveProperty<bool> IsLeaderBoardPinned => isLeaderBoardPinned;
        public IReadOnlyReactiveProperty<bool> IsLeaderBoardFullScreen => isLeaderBoardFullScreen;
        public readonly IReadOnlyReactiveProperty<bool> ShouldShowLeaderBoard;


        public UIModel()
        {
            isLeaderBoardPinned = new ReactiveProperty<bool>();
            isLeaderBoardFullScreen = new ReactiveProperty<bool>(true);
            isLeaderBoardHoveredOver = new ReactiveProperty<bool>();
            ShouldShowLeaderBoard = isLeaderBoardPinned
                .CombineLatest(isLeaderBoardHoveredOver, (isPinned, isHovered) => isPinned || isHovered)
                .ToReadOnlyReactiveProperty();
                
        }

        public void SetLeaderBoardHoveredOver(bool hovered)
        {
            isLeaderBoardHoveredOver.Value = hovered;
            if (!hovered)
            {
                SetLeaderBoardFullScreen(false);
            }
        }

        public void SetLeaderBoardPinned(bool isPinned)
        {
            isLeaderBoardPinned.Value = isPinned;
        }

        public void SetLeaderBoardFullScreen(bool isFullScreen)
        {
            isLeaderBoardFullScreen.Value = isFullScreen;
        }
    }
}