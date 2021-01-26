using Assets.Scripts.Features.MVC;
using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Features.Audio
{
    [UsedImplicitly]
    public class AudioModel : AbstractModel
    {
        private readonly Subject<AudioEffectData> onPlayAudioEffect;
        public Subject<AudioEffectData> OnPlayAudioEffect => onPlayAudioEffect;

        public AudioModel()
        {
            onPlayAudioEffect = new Subject<AudioEffectData>();
        }

        public void PlayEffect(AudioEffectData effect)
        {
            onPlayAudioEffect.OnNext(effect);
        }
    }
}