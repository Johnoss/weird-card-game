using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Features.Actor;
using Assets.Scripts.Features.Card.Factory;
using Assets.Scripts.Features.Gauge.Factory;
using Assets.Scripts.Features.SceneControl;
using Assets.Scripts.Features.Score;
using Assets.Scripts.Features.Score.Factory;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Initializers
{
    [UsedImplicitly]
    public class GameInitializer : IInitializable
    {
        [Inject] private GaugeFactory gaugeFactory;
        [Inject] private CardFactory cardFactory;
        [Inject] private ScoreFactory scoreFactory;

        [Inject] private ActorContainerView actorContainerView;
        [Inject] private SceneTransitionView sceneTransitionView;

        [Inject] private GameProgressModel gameProgressModel;

        [Inject]
        public void Initialize()
        {
            Observable.FromCoroutine(StartInitialize).Subscribe();
        }

        private IEnumerator StartInitialize()
        {
            #region Gauges

            var gaugesContainerBundle = gaugeFactory.CreateGaugesContainerBundle();
            var gaugeModels = gaugesContainerBundle.Model.GaugeBundles.Select(model => model.Model).ToList();

            yield return new WaitForEndOfFrame();
            #endregion


            #region Cards

            var cardBundles = cardFactory.CreateCards();
            cardFactory.CreateHand(cardBundles, gaugesContainerBundle.Controller);

            yield return new WaitForEndOfFrame();
            #endregion

            #region Actors

            actorContainerView.Setup(gaugeModels);

            yield return new WaitForEndOfFrame();
            #endregion

            #region Score

            var playerScoreModel = scoreFactory.CreatePlayerScore(gaugeModels);
            var scoreModels = new List<ScoreModel>
            {
                playerScoreModel
            };
            scoreModels.AddRange(scoreFactory.CreateOpponentScores());

            scoreFactory.SetupGameSummary();


            _ = new LeaderBoardController(scoreModels, gameProgressModel);

            yield return new WaitForEndOfFrame();
            #endregion

            sceneTransitionView.Fade(false);

            Debug.Log("Game Initialized");
        }
    }
}