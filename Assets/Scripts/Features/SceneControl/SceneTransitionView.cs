using System;
using Features.Animations.SceneTransition;
using Features.Animations.SceneTransition.Config;
using Features.MVC;
using UniRx;
using UnityEngine;

namespace Features.SceneControl
{
    public class SceneTransitionView : AbstractView
    {
        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animation")]
        [SerializeField] private SceneTransitionTweener sceneTransitionTweener;
        [SerializeField] private SceneTransitionTweenerConfig tweenerConfig;

        private readonly CompositeDisposable fadeDisposer = new CompositeDisposable();

        public void Fade(bool fadeIn)
        {
            fadeDisposer.Clear();

            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = fadeIn ? 0 : 1;

            sceneTransitionTweener.Animate(fadeIn);

            if(!fadeIn)
            {
                SetupDisableSubscription();
            }

        }

        private void SetupDisableSubscription()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(tweenerConfig.AnimationDurationSeconds))
                .Take(1)
                .Subscribe(_ => DisableCanvasGroup()).AddTo(fadeDisposer);
        }

        private void DisableCanvasGroup()
        {
            canvasGroup.gameObject.SetActive(false);
        }
    }
}