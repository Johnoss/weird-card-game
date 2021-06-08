using System.Collections.Generic;
using Features.Animations.Hand;
using Features.Card.Config;
using Features.Card.SelectedCard;
using Features.MVC;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Card.Hand
{
    public class CardSlotsView : AbstractView, IInitializable
    {
        [Header("Components")]
        [SerializeField] private Transform selectedCardParent;
        [SerializeField] private Transform slotsParent;

        [Header("Animation")]
        [SerializeField] private HandSelectedTweener handSelectedTweener;
        [SerializeField] private HandPlayedTweener handPlayedTweener;

        [Header("Config")]
        [SerializeField] private DeckConfig deckConfig;

        public List<RectTransform> CardSlots { get; } = new List<RectTransform>();
        public Transform SelectedCardParent => selectedCardParent;

        [Inject] private SelectedCardModel selectedCardModel;

        [Inject]
        public void Initialize()
        {
            for (var i = 0; i < deckConfig.HandSize; i++)
            {
                var slotGameObjectName = string.Format(deckConfig.SlotGameObjectName, i);
                var localEulerAngles = Vector3.forward * GetEulerZForIndex(i);

                var slotObject = new GameObject(slotGameObjectName, typeof(RectTransform));
                var slotTransform = slotObject.transform;
                slotTransform.localEulerAngles = localEulerAngles;
                slotTransform.SetParent(slotsParent);

                CardSlots.Add(slotObject.GetComponent<RectTransform>());
            }

            Setup();
        }

        private void Setup()
        {
            selectedCardModel.IsCardSelected.Subscribe(_ => AnimateSelected()).AddTo(this);
            selectedCardModel.OnPlaySelectedCard.DelayFrame(1).Subscribe(_ => AnimatePlayed()).AddTo(this);
        }

        private void AnimateSelected()
        {
            handSelectedTweener.AnimateSelected(selectedCardModel.IsCardSelected.Value);
        }

        private void AnimatePlayed()
        {
            handPlayedTweener.AnimatePlayed();
        }

        private float GetEulerZForIndex(int index)
        {

            var range = deckConfig.HandArcRotationRange.max - deckConfig.HandArcRotationRange.min;
            var absoluteEulerZ = deckConfig.HandSize > 1
                ? range / (deckConfig.HandSize - 1) * index
                : 0f; //Avoid division by 0, in case of 1 card method returns the middle of the range
            var relativeEulerZ = -(deckConfig.HandArcRotationRange.min + absoluteEulerZ);

            return relativeEulerZ;
        }
    }
}