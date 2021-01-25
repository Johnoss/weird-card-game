using Assets.Scripts.Features.Animations.Card;
using Assets.Scripts.Features.Animations.SceneTransition.Config;
using Assets.Scripts.Features.Card.Hand;
using Assets.Scripts.Features.Card.SelectedCard;
using Assets.Scripts.Features.Gauge;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Features.SceneControl;
using Assets.Scripts.Features.SceneControl.Config;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Card
{
    public class MenuCardView : AbstractView, IInitializable
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<GaugeModel, GaugesConfig.GaugeSetting, GaugeView> { }

        [Header("Scene References")]
        [SerializeField] private CardSlotsView cardSlotsView;

        [Header("Definition")]
        [SerializeField] private int index;

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

        [Inject] private SceneTransitionView sceneTransitionView;

        private bool IsSelected => selectedCardModel.SelectedCardIndex.Value == index;

        [Inject]
        public void Initialize()
        {
            Observable.TimerFrame(1)
                .Take(1)
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