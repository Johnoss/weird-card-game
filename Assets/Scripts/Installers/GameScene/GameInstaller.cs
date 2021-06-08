using Features.Initializers;
using Features.SceneControl;
using Features.Score;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneTransitionView sceneTransitionView;
        [SerializeField] private GameSummaryView gameSummaryView;

        public override void InstallBindings()
        {
            Container.Bind<GameInitializer>().AsSingle().NonLazy();

            Container.BindInstance(sceneTransitionView).AsSingle();
            Container.BindInstance(gameSummaryView).AsSingle();

            Container.Bind<GameProgressModel>().AsSingle();
            Container.Bind<GameProgressController>().AsSingle();
        }
    }
}