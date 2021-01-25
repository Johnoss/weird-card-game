using Assets.Scripts.Features.Actor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
{
    public class ActorInstaller : MonoInstaller
    {
        [SerializeField] private ActorContainerView actorContainerView;

        public override void InstallBindings()
        {
            Container.BindInstance(actorContainerView);
        }
    }
}