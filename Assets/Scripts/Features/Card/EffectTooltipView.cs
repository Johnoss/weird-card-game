using System;
using System.Linq;
using Features.Animations.Card.Config;
using Features.Card.Effects;
using Features.Gauge.Config;
using Features.MVC;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Card
{
    public class EffectTooltipView : AbstractView
    {

        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI effectWeightText;
        [SerializeField] private Image iconImage;

        [Header("Configs")]
        [SerializeField] private GaugesConfig gaugesConfig;
        [SerializeField] private CommonStatsConfig commonStatsConfig;
        [SerializeField] private PlayCardTweenerConfig playCardTweenerConfig;

        private CardModel cardModel;
        private int actorIndex;

        private GaugesConfig.GaugeSetting gaugeSetting;

        public void Setup(CardModel cardModel, int actorIndex)
        {
            this.cardModel = cardModel;
            this.actorIndex = actorIndex;

            gaugeSetting = gaugesConfig.GaugeSettings[actorIndex];

            this.cardModel.CardData
                .Where(data => data != null)
                .Take(1)
                .Subscribe(_ => UpdateEffect()).AddTo(this);

            cardModel.CardData
                .Delay(TimeSpan.FromSeconds(playCardTweenerConfig.AnimationDurationSeconds))
                .Subscribe(_ => UpdateEffect()).AddTo(this);
        }

        private void UpdateEffect()
        {
            var effect = cardModel.CardEffects[actorIndex];
            var hasEffect = effect != CardEffect.None;
            gameObject.SetActive(hasEffect);
            if (!hasEffect)
            {
                return;
            }

            var reactionExpression = gaugeSetting.ReactionSettings.FirstOrDefault(reaction => reaction.Effect == effect)
                ?.ReactionSprite;
            var effectSetting = commonStatsConfig.GetEffectSetting(effect);

            effectWeightText.text = effectSetting.EffectDescription;
            effectWeightText.color = effectSetting.EffectTextColor;

            iconImage.overrideSprite = reactionExpression;
        }
    }
}