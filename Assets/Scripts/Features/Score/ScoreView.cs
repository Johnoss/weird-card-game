using Assets.Scripts.Features.Animations.Score;
using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Score
{
    public class ScoreView : AbstractView
    {
        [UsedImplicitly]
        public class ViewFactory : PlaceholderFactory<IScoreModel, ScoreView> { }

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI playerScoreText;

        [Header("Decorations")]
        [SerializeField] private Image separatorImage;

        [Header("Animation")]
        [SerializeField] private ScoreOrderTweener scoreOrderTweener;

        private ScoreModel scoreModel;

        [Inject]
        public void Construct(ScoreModel scoreModel)
        {
            this.scoreModel = scoreModel;

            playerScoreText.color = scoreModel.TextColor;
            playerNameText.color = scoreModel.TextColor;
            separatorImage.color = scoreModel.TextColor;

            scoreModel.DisplayScore.Subscribe(_ => UpdateScore()).AddTo(this);
            scoreModel.Order.Subscribe(_ => UpdateOrder()).AddTo(this);
            scoreModel.ShouldShow.Subscribe(_ => UpdateShouldShow()).AddTo(this);

            UpdateOrder();
            UpdateScore();
        }

        private void UpdateShouldShow()
        {
            //TODO implement tweener
            gameObject.SetActive(false);
        }

        private void UpdateOrder()
        {
            var order = scoreModel.Order.Value;
            var targetAnchoredY = -185 * (order - 1); //TODO proper positioning
            scoreOrderTweener.Animate(targetAnchoredY);

            var formattedName = $"{order}. {scoreModel.PlayerName}";
            playerNameText.text = formattedName;
        }

        private void UpdateScore()
        {
            var formattedScore = $"{scoreModel.DisplayScore.Value} Listeners";
            playerScoreText.text = formattedScore;
        }
    }
}