using Assets.Scripts.Features.Animations.Config;
using Assets.Scripts.Features.Animations.UI.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class PinTweener : AbstractTweener
    {
        [Header("Components")]
        [SerializeField] private RectTransform pinTransform;

        [Header("Configs")]
        [SerializeField] private AbstractTweenerConfig precedingTweenerConfig;
        [SerializeField] private PinTweenerConfig tweenerConfig;

        public void Animate(bool show)
        {
            RestartAnimation();

            var targetAnchorPosition = show
                ? Vector2.zero
                : tweenerConfig.PinHiddenAnchorPosition;

            var delaySeconds = show && precedingTweenerConfig != null
                ? precedingTweenerConfig.AnimationDurationSeconds
                : 0f;

            Tweener = pinTransform
                .DOAnchorPos(targetAnchorPosition, tweenerConfig.AnimationDurationSeconds)
                .SetDelay(delaySeconds)
                .SetEase(tweenerConfig.Ease);
        }

        private void RestartAnimation()
        {
            KillTweener();
        }
    }
}
