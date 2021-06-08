using Features.Initializers;
using Features.SceneControl;
using UnityEngine;
using Zenject;

namespace Installers.MenuScene
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private SceneTransitionView sceneTransitionView;

        public override void InstallBindings()
        {
            Container.Bind<MenuInitializer>().AsSingle().NonLazy();

            Container.BindInstance(sceneTransitionView).AsSingle();
        }
    }
}