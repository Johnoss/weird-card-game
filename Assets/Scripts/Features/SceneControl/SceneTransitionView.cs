using System;
using Assets.Scripts.Features.Animations.SceneTransition;
using Assets.Scripts.Features.Animations.SceneTransition.Config;
using Assets.Scripts.Features.MVC;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Features.SceneControl
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