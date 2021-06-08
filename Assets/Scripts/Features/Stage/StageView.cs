using Features.Animations.Stage;
using Features.MVC;
using Features.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Stage
{
    public class StageView : AbstractView, IInitializable
    {
        [Header("Scene References")]
        [SerializeField] private Transform zoomedInParent;
        [SerializeField] private Transform zoomedOutParent;

        [Header("Animation")]
        [SerializeField] private StageCameraTweener stageCameraTweener;

        [Inject] private UIModel uiModel;

        [Inject]
        public void Initialize()
        {
            uiModel.ShouldShowLeaderBoard.Subscribe(_ => UpdateStageCamera()).AddTo(this);
        }

        private void UpdateStageCamera()
        {
            var isLeaderBoardShown = uiModel.ShouldShowLeaderBoard.Value;
            var targetParent = isLeaderBoardShown
                ? zoomedOutParent
                : zoomedInParent;

            transform.SetParent(targetParent);

            stageCameraTweener.Animate();
        }
    }
}