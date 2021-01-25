using Assets.Scripts.Features.Initializers;
using Assets.Scripts.Features.SceneControl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.MenuScene
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