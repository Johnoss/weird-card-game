using Assets.Scripts.Features.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.Persistent
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioView audioView;

        public override void InstallBindings()
        {
            Container.Bind<AudioModel>().AsSingle();
            Container.Bind<AudioController>().AsSingle();
            Container.BindInstance(audioView).AsSingle();
        }
    }
}