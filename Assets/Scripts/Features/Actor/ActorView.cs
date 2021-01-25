using Assets.Scripts.Features.Actor.Config;
using Assets.Scripts.Features.MVC;
using System;
using System.Linq;
using Assets.Scripts.Features.Animations.Actor;
using Assets.Scripts.Features.Gauge;
using Assets.Scripts.Features.Gauge.Config;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Actor
{
    public class ActorView : AbstractView
    {
        [Header("Components")]
        [SerializeField] private Image expressionImage;

        [Header("Definition")]
        [SerializeField] private int actorIndex;

        [Header("Animation")]
        [SerializeField] private ActorShakeTweener shakeTweener;

        [Inject] private ActorConfig actorConfig;
        [Inject] private CommonStatsConfig commonStatsConfig;
        [Inject] private GaugesConfig gaugesConfig;

        private GaugesConfig.GaugeSetting gaugeSetting;
        private ActorModel actorModel;
        private GaugeModel gaugeModel;

        public void Setup(GaugeModel gaugeModel)
        {
            this.gaugeModel = gaugeModel;

            gaugeSetting = gaugesConfig.GaugeSettings[actorIndex];

            gaugeModel.GaugeValue.Subscribe(_ => OnReactionActivate()).AddTo(this); //First subscription sets the override expression
            gaugeModel.GaugeValue
                .Delay(TimeSpan.FromSeconds(actorConfig.EffectExpressionSeconds)) //Second subscription resets the expression after the configured time
                .Subscribe(_ => ResetEffectExpression()).AddTo(this);

            gaugeModel.ScoreModifier.Subscribe(_ => UpdateExpression()).AddTo(this);
        }

        private void UpdateExpression()
        {
            var gaugeValue = gaugeModel.GaugeValue.Value;
            var expressionIndex = commonStatsConfig.GetIndexOfValue(gaugeValue);

            var expressionSprite = gaugeSetting.Expressions[expressionIndex];

            expressionImage.sprite = expressionSprite;

            var shouldShake = commonStatsConfig.GetScoreSettingForValue(gaugeValue).ShouldShake;
            shakeTweener.ToggleShaking(shouldShake);
        }

        private void OnReactionActivate()
        {
            var cardEffect = gaugeModel.LatestCardEffect;
            var reactionExpression = gaugeSetting.ReactionSettings.FirstOrDefault(reaction => reaction.Effect == cardEffect)?.ReactionSprite;

            expressionImage.overrideSprite = reactionExpression;
        }

        private void ResetEffectExpression()
        {
            expressionImage.overrideSprite = null;
        }
    }
}