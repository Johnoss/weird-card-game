using Assets.Scripts.Features.Animations.Stage.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Stage
{
    [RequireComponent(typeof(RectTransform))]
    public class StageCameraTweener : AbstractTweener
    {
        [Header("Components")]
        [SerializeField] private RectTransform stageParentTransform;

        [Header("Configs")]
        [SerializeField] private StageCameraTweenerConfig tweenerConfig;

        private Tweener anchorPositionTweener;

        public void Animate()
        {
            RestartAnimation();

            Tweener = stageParentTransform
                .DOScale(Vector2.one, tweenerConfig.AnimationDurationSeconds)
                .SetEase(tweenerConfig.Ease);

            anchorPositionTweener = stageParentTransform
                .DOBlendableLocalMoveBy(-stageParentTransform.anchoredPosition, tweenerConfig.AnimationDurationSeconds)
                .SetEase(tweenerConfig.Ease);
        }

        private void RestartAnimation()
        {
            anchorPositionTweener?.Kill();
            KillTweener();
        }
    }
}
