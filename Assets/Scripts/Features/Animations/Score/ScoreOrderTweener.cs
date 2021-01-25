using Assets.Scripts.Features.Animations.Score.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Score
{
    [RequireComponent(typeof(RectTransform))]
    public class ScoreOrderTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private ScoreTweenerConfig scoreTweenerConfig;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Animate(float targetAnchoredY)
        {
            KillTweener();

            Tweener = rectTransform
                .DOAnchorPosY(targetAnchoredY, scoreTweenerConfig.AnimationDurationSeconds)
                .SetEase(scoreTweenerConfig.Ease);
        }
    }
}
