using System;
using Features.MVC;
using Features.SceneControl.Config;
using JetBrains.Annotations;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;
using Scene = Features.SceneControl.Config.Scene;

namespace Features.SceneControl
{
    [UsedImplicitly]
    public class SceneController : AbstractController
    {
        [Inject] private SceneModel sceneModel;
        [Inject] private SceneConfig sceneConfig;

        private readonly CompositeDisposable sceneChangeDisposer = new CompositeDisposable();

        public void LoadScene(Scene scene, float delaySeconds = 0f, bool unloadCurrentScene = true)
        {
            sceneChangeDisposer.Clear();

            Observable
                .Timer(TimeSpan.FromSeconds(delaySeconds))
                .Take(1)
                .Subscribe(_ => DelayedLoadScene(scene, unloadCurrentScene)).AddTo(sceneChangeDisposer);
            ;
        }

        private void DelayedLoadScene(Scene scene, bool unloadCurrentScene)
        {
            var buildIndex = sceneConfig.GetBuildIndexForScene(scene);

            SceneManager.LoadScene(buildIndex);

            if (unloadCurrentScene)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            }
        }
    }
}