using Assets.Scripts.Features.Animations.Card;
using Assets.Scripts.Features.Animations.Card.Config;
using Assets.Scripts.Features.Card.Config;
using Assets.Scripts.Features.Card.SelectedCard;
using Assets.Scripts.Features.MVC;
using Assets.Scripts.Utilities.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Assets.Scripts.Features.Audio;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Card
{
    public class CardView : AbstractView
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<CardModel, CardController, SelectedCardModel, Transform, CardView> { }

        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI cardTitleText;
        [SerializeField] private TextMeshProUGUI cardDescriptionText;
        [SerializeField] private Image cardCoverImage;
        [SerializeField] private Image cardBackgroundImage;

        [Header("Components")]
        [SerializeField] private RectTransform cardTransform;
        [SerializeField] private List<EffectTooltipView> effectViews;

        [Header("Interaction")]
        [SerializeField] private Button cardButton;

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
        private CardController cardController;
        private Transform selectedCardParent;

        private Vector3 defaultAnchoredPosition;
        private Vector3 defaultRotation;
        private Vector3 defaultCardScale;
        private AudioController audioController;

        private float PlaySequenceSeconds => drawCardTweenerConfig.GetDelaySecondsForIndex(cardModel.CardIndex) +
                                             drawCardTweenerConfig.AnimationDurationSeconds +
                                             playCardTweenerConfig.AnimationDurationSeconds;

        [Inject]
        public void Construct(CardModel cardModel, CardController cardController, SelectedCardModel selectedCardModel,
            AudioController audioController, Transform selectedCardParent)
        {
            this.cardModel = cardModel;
            this.selectedCardModel = selectedCardModel;
            this.cardController = cardController;
            this.selectedCardParent = selectedCardParent;
            this.audioController = audioController;

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

            cardModel.CardData
                .Where(data => data != null)
                .Take(1)
                .Subscribe(_ => UpdateCard()).AddTo(this);

            cardModel.CardData
                .Skip(2)
                .Delay(TimeSpan.FromSeconds(playCardTweenerConfig.AnimationDurationSeconds))
                .Subscribe(_ => UpdateCard()).AddTo(this);

            cardButton.OnClickAsObservable().Subscribe(_ => OnCardClicked()).AddTo(this);

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

        [UsedImplicitly]
        public void OnCardHovered()
        {
            if (!cardModel.IsSuspended.Value)
            {
                cardController.SelectCard();
            }
        }

        private void ResetTweener()
        {
            selectedCardTweener.ResetTweeners();
        }

        private void OnCardClicked()
        {
            if (cardModel.IsSelected.Value)
            {
                audioController.PlayClip(AudioEffectType.CardPlay);
                cardController.PlayCard();
                playCardTweener.Animate();
                return;
            }

            OnCardHovered();
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