using Assets.Scripts.Features.Animations.Card;
using Assets.Scripts.Features.Animations.SceneTransition.Config;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Features.SceneControl;
using Assets.Scripts.Features.SceneControl.Config;
using Assets.Scripts.Features.UI;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Score
{
    public class GameSummaryView : AbstractView
    {
        [Header("Scene References")]
        [SerializeField] private Transform shownParent;
        [SerializeField] private Transform hiddenParent;

        [Header("Interaction")]
        [SerializeField] private Button menuButton;
        [SerializeField] private Button closeButton;

        [Header("Grouping")]
        [SerializeField] private GameObject summaryItemsParent;

        [Header("Animation")]
        [SerializeField] private TranslateToParentTweener gameSummaryTweener;

        [Header("Config")]
        [SerializeField] private SceneTransitionTweenerConfig sceneTransitionTweenerConfig;

        private GameProgressModel gameProgressModel;

        [Inject] private SceneTransitionView sceneTransitionView; 
        [Inject] private UIController uiController; 
        [Inject] private SceneController sceneController;

        public void Setup(GameProgressModel gameProgressModel)
        {
            this.gameProgressModel = gameProgressModel;

            gameProgressModel.HasPlayerWon.Subscribe(_ => UpdateShowPanel()).AddTo(this);

            menuButton.OnClickAsObservable().Subscribe(_ => LoadMenuScene()).AddTo(this);
            closeButton.OnClickAsObservable().Subscribe(_ => OnCloseOverlay()).AddTo(this);
        }

        [UsedImplicitly]
        public void OnCloseOverlay()
        {
            closeButton.gameObject.SetActive(false);
        }

        private void LoadMenuScene()
        {
            sceneTransitionView.Fade(true); //TODO extract logic to a controller so the view doesn't reference a view

            var delaySeconds = sceneTransitionTweenerConfig.AnimationDurationSeconds;
            sceneController.LoadScene(Scene.Menu, delaySeconds);
        }

        private void UpdateShowPanel()
        {
            var shouldShow = gameProgressModel.HasPlayerWon.Value;

            summaryItemsParent.SetActive(shouldShow);

            var targetParent = shouldShow
                ? shownParent
                : hiddenParent;

            transform.SetParent(targetParent);
            gameSummaryTweener.Animate();
        }
    }
}