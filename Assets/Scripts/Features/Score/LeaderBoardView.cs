using Assets.Scripts.Features.Animations.Card;
using Assets.Scripts.Features.Animations.UI;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Features.UI;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Score
{
    public class LeaderBoardView : AbstractView, IInitializable
    {
        [Header("Scene References")]
        [SerializeField] private Transform dockedParent;
        [SerializeField] private Transform hiddenParent;
        [SerializeField] private Transform fullScreenParentParent;

        [Header("Interaction")]
        [SerializeField] private Button pinButton;
        [SerializeField] private Button closeButton;

        [Header("Visuals")]
        [SerializeField] private Sprite pinDownSprite;
        [SerializeField] private Image pinImage;

        [Header("Animation")]
        [SerializeField] private TranslateToParentTweener leaderBoardsTweener;
        [SerializeField] private PinTweener pinTweener;

        [Inject] private UIController uiController;
        [Inject] private UIModel uiModel;
        [Inject] private GameProgressModel gameProgressModel;

        private bool LockInteractability =>
            uiModel.IsLeaderBoardFullScreen.Value || gameProgressModel.HasPlayerWon.Value;

        [Inject]
        public void Initialize()
        {
            uiModel
                .ShouldShowLeaderBoard
                .Merge(uiModel.IsLeaderBoardFullScreen)
                .Subscribe(_ => UpdateShowPanel()).AddTo(this);

            uiModel.IsLeaderBoardPinned.Subscribe(_ => UpdatePinButton()).AddTo(this);

            pinButton
                .OnClickAsObservable()
                .Where(_ => !LockInteractability)
                .Subscribe(_ => uiController.ToggleLeaderBoardPinned()).AddTo(this);

            closeButton.OnClickAsObservable().Subscribe(_ => uiController.SetLeaderBoardFullScreen(false)).AddTo(this);

            uiModel.IsLeaderBoardFullScreen
                .Where(isFullScreen => !isFullScreen)
                .Take(1)
                .Subscribe(_ => closeButton.gameObject.SetActive(false)).AddTo(this);
        }

        [UsedImplicitly]
        public void OnPointerEnter()
        {
            if (!LockInteractability)
            {
                uiController.SetLeaderBoardHoveredOver(true);
            }
        }

        [UsedImplicitly]
        public void OnPointerExit()
        {
            if (!LockInteractability)
            {
                uiController.SetLeaderBoardHoveredOver(false);
            }
        }

        private void UpdateShowPanel()
        {
            Transform targetParent;
            if (LockInteractability)
            {
                targetParent = fullScreenParentParent;
            }
            else
            {
                var shouldShow = uiModel.ShouldShowLeaderBoard.Value;
                targetParent = shouldShow
                    ? dockedParent
                    : hiddenParent;

                pinTweener.Animate(shouldShow);
            }

            transform.SetParent(targetParent);
            leaderBoardsTweener.Animate();
        }

        private void UpdatePinButton()
        {
            var isPinDown = uiModel.IsLeaderBoardPinned.Value;

            pinImage.overrideSprite = isPinDown
                ? pinDownSprite
                : null;
        }
    }
}