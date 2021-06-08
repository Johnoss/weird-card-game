using System;
using System.Collections.Generic;
using Features.Animations.Card;
using Features.Animations.Card.Config;
using Features.Audio;
using Features.Card.Config;
using Features.Card.SelectedCard;
using Features.MVC;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;
using Zenject;

namespace Features.Card
{
    public class CardView : AbstractView
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<CardModel, CardController, SelectedCardModel, CardGesturesModel,
            DraggableController, Transform, CardView>
        { }

        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI cardTitleText;
        [SerializeField] private TextMeshProUGUI cardDescriptionText;
        [SerializeField] private Image cardCoverImage;
        [SerializeField] private Image cardBackgroundImage;

        [Header("Components")]
        [SerializeField] private RectTransform cardTransform;
        [SerializeField] private List<EffectTooltipView> effectViews;
        [SerializeField] private DraggableView draggableView;

        [Header("Animation")]
        [SerializeField] private TranslateToParentTweener selectedCardTweener;
        [SerializeField] private PlayCardTweener playCardTweener;
        [SerializeField] private DrawCardTweener drawCardTweener;

        [Header("Configs")]
        [SerializeField] private DeckConfig deckConfig;
        [SerializeField] private PlayCardTweenerConfig playCardTweenerConfig;
        [SerializeField] private TranslateToParentTweenerConfig selectedCardTweenerConfig;
        [SerializeField] private DrawCardTweenerConfig drawCardTweenerConfig;


        private CardModel cardModel;
        private SelectedCardModel selectedCardModel;
        private CardGesturesModel cardGesturesModel;
        private CardController cardController;
        private SelectedCardController selectedCardController;
        private Transform selectedCardParent;

        private Vector3 defaultAnchoredPosition;
        private Vector3 defaultRotation;
        private Vector3 defaultCardScale;
        private AudioController audioController;
        private DraggableController draggableController;

        private float PlaySequenceSeconds => drawCardTweenerConfig.GetDelaySecondsForIndex(cardModel.CardIndex) +
                                             drawCardTweenerConfig.AnimationDurationSeconds +
                                             playCardTweenerConfig.AnimationDurationSeconds;

        [Inject]
        public void Construct(CardModel cardModel, CardController cardController, SelectedCardModel selectedCardModel,
            CardGesturesModel cardGesturesModel, AudioController audioController,
            DraggableController draggableController, SelectedCardController selectedCardController,
            Transform selectedCardParent)
        {
            this.cardModel = cardModel;
            this.selectedCardModel = selectedCardModel;
            this.cardGesturesModel = cardGesturesModel;
            this.cardController = cardController;
            this.selectedCardParent = selectedCardParent;
            this.audioController = audioController;
            this.draggableController = draggableController;
            this.selectedCardController = selectedCardController;

            defaultAnchoredPosition = cardTransform.anchoredPosition;
            defaultRotation = cardTransform.localEulerAngles;
            defaultCardScale = cardTransform.localScale;

            Setup();
        }

        private void Setup()
        {
            for (var i = 0; i < effectViews.Count; i++)
            {
                effectViews[i].Setup(cardModel, i);
            }
            
            draggableView.Setup(draggableController);

            SubscribeGestures();
            
            cardModel.CardData
                .Where(data => data != null)
                .Take(1)
                .Subscribe(_ => UpdateCard()).AddTo(this);

            cardModel.CardData
                .Skip(2)
                .Delay(TimeSpan.FromSeconds(playCardTweenerConfig.AnimationDurationSeconds))
                .Subscribe(_ => UpdateCard()).AddTo(this);

            selectedCardModel
                .OnPlaySelectedCard
                .Subscribe(_ => cardController.SuspendCard(PlaySequenceSeconds)).AddTo(this);

            cardModel.IsSelected
                .Skip(1)
                .Subscribe(_ => UpdateIsSelected()).AddTo(this);

            cardModel.OnCardPlayed
                .DelayFrame(1)
                .Subscribe(_ => ResetTweener()).AddTo(this);

            cardModel
                .OnCardPlayed
                .Delay(TimeSpan.FromSeconds(playCardTweenerConfig.AnimationDurationSeconds))
                .Subscribe(_ => ResetCard()).AddTo(this);

            ResetCard();
        }

        private void SubscribeGestures()
        {
            cardGesturesModel.OnSwipe.Subscribe(OnCardSwiped).AddTo(this);
        }

        private void OnCardSwiped(SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.Up:
                    OnCardUp();
                    break;
                case SwipeDirection.Right:
                {
                    OffsetSelectedCard(1);
                    break;
                }
                case SwipeDirection.Down:
                    selectedCardController.DeselectCard();
                    break;
                case SwipeDirection.Left:
                {
                    OffsetSelectedCard(-1);
                    break;
                }
                default:
                    return;
            }
        }

        private void OffsetSelectedCard(int offset)
        {
            if (!cardModel.IsSelected.Value) return;
            
            var newIndex = (int) Mathf.Repeat(cardModel.CardIndex + offset, deckConfig.HandSize);
            selectedCardController.SetSelectedCard(newIndex);
        }

        private void ResetTweener()
        {
            selectedCardTweener.ResetTweeners();
        }

        private void OnCardUp()
        {
            if (cardModel.IsSelected.Value)
            {
                audioController.PlayClip(AudioEffectType.CardPlay);
                cardController.PlayCard();
                playCardTweener.Animate();
                return;
            }
            
            cardController.SelectCard();
        }

        private void UpdateIsSelected()
        {

            var isSelected = cardModel.IsSelected.Value;
            var targetParent = isSelected
                ? selectedCardParent
                : transform;

            selectedCardTweener.transform.SetParent(targetParent);

            selectedCardTweener.Animate();

            cardController.SuspendCard(selectedCardTweenerConfig.SuspendSelectedSeconds);
        }

        private void UpdateCard()
        {
            cardBackgroundImage.sprite = deckConfig.CardVariantsSprites.RandomElement();

            var cardData = cardModel.CardData.Value;

            cardTitleText.text = cardData.TitleText;
            cardDescriptionText.text = cardData.DescriptionText;
            cardCoverImage.overrideSprite = cardData.IconSprite;

            drawCardTweener.Animate(cardModel.CardIndex);
            audioController.PlayClip(AudioEffectType.CardDeal,
                drawCardTweenerConfig.GetDelaySecondsForIndex(cardModel.CardIndex));
        }

        private void ResetCard()
        {
            cardTransform.SetParent(transform);
            cardTransform.anchoredPosition = defaultAnchoredPosition;
            cardTransform.localScale = defaultCardScale;
            cardTransform.localEulerAngles = defaultRotation;
        }
    }
}