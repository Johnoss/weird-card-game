using DG.Tweening;
using Features.Animations.Actor.Config;
using UnityEngine;

namespace Features.Animations.Actor
{
    [RequireComponent(typeof(RectTransform))]
    public class ActorShakeTweener : AbstractTweener
    {
        [Header("Configs")]
        [SerializeField] private ActorShakeTweenerConfig tweenConfig;

        private Tweener gaugeTweener;

        private RectTransform rectTransform;
        private Vector2 defaultLocalPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultLocalPosition = rectTransform.localPosition;
        }

        public void ToggleShaking(bool shouldShake)
        {

            ResetAnimation();

            if (!shouldShake)
            {
                return;
            }

            Tweener = rectTransform
                .DOShakePosition(tweenConfig.AnimationDurationSeconds, tweenConfig.ShakeStrength, tweenConfig.Vibrato,
                    tweenConfig.Randomness, false, false)
                .SetLoops(-1)
                .SetEase(tweenConfig.Ease);
        }

        private void ResetAnimation()
        {
            KillTweener();
            if(rectTransform != null)
            {
                rectTransform.localPosition = defaultLocalPosition;
            }
        }
    }
}
