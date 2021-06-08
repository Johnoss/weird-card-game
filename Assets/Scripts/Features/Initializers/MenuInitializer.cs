using System.Collections;
using Features.SceneControl;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Initializers
{
    [UsedImplicitly]
    public class MenuInitializer : IInitializable
    {
        [Inject] private SceneTransitionView sceneTransitionView;

        [Inject]
        public void Initialize()
        {
            Observable.FromCoroutine(StartInitialize).Subscribe();
        }

        private IEnumerator StartInitialize()
        {
            yield return new WaitForEndOfFrame();

            sceneTransitionView.Fade(false);

            Debug.Log("Menu Initialized");
        }
    }
}