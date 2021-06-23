using Features.Animations.Card;
using Features.Animations.SceneTransition.Config;
using Features.Card.Hand;
using Features.Card.SelectedCard;
using Features.Gauge;
using Features.Gauge.Config;
using Features.MVC;
using Features.SceneControl;
using Features.SceneControl.Config;
using Features.SessionSettings;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Card
{
    public class MenuCardView : AbstractView, IInitializable
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<GaugeModel, GaugesConfig.GaugeSetting, GaugeView> { }

        [Header("Scene References")]
        [SerializeField] private CardSlotsView cardSlotsView;

        [Header("Definition")]
        [SerializeField] private int index;
        [SerializeField] private GameMode gameMode;

        [Header("Interaction")]
        [SerializeField] private Button cardButton;

        [Header("Animation")]
        [SerializeField] private TranslateToParentTweener selectedCardTweener;
        [SerializeField] private PlayCardTweener playCardTweener;

        [Header("Config")]
        [SerializeField] private SceneTransitionTweenerConfig transitionTweenerConfig;

        [Inject] private SelectedCardModel selectedCardModel;
        [Inject] private SelectedCardController selectedCardController;
        [Inject] private SceneController sceneController;
        [Inject] private GameModeController gameModeController;
        
        
        [Inject] private SceneTransitionView sceneTransitionView;

        private bool IsSelected => selectedCardModel.SelectedCardIndex.Value == index;

        [Inject]
        public void Initialize()
        {
            Observable.TimerFrame(1)
                .Take(1)
                .Where(_ => cardSlotsView.CardSlots.Count > index)
                .Subscribe(_ => transform.SetParent(cardSlotsView.CardSlots[index], false)).AddTo(this);

            Setup();
        }

        private void Setup()
        {
            selectedCardModel
                .SelectedCardIndex
                .Skip(1)
                .Subscribe(_ => UpdateIsSelected()).AddTo(this);

            cardButton.OnClickAsObservable().Subscribe(_ => OnCardClicked()).AddTo(this);
        }

        [UsedImplicitly]
        public void OnCardHovered()
        {
            selectedCardController.SetSelectedCard(index);
        }


        private void OnCardClicked()
        {
            if (IsSelected)
            {
                playCardTweener.Animate();
                sceneTransitionView.Fade(true);
                
                gameModeController.SetGameMode(gameMode);
                
                sceneController.LoadScene(Scene.Game, transitionTweenerConfig.AnimationDurationSeconds);
            }

            OnCardHovered();
        }

        private void UpdateIsSelected()
        {
            var tweenerParent = IsSelected
                ? cardSlotsView.SelectedCardParent
                : transform;

            selectedCardTweener.transform.SetParent(tweenerParent);
            selectedCardTweener.Animate();
        }
    }
}