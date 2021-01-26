using Assets.Scripts.Features.Audio.Config;
using Assets.Scripts.Features.MVC;
using JetBrains.Annotations;
using System;
using Zenject;

namespace Assets.Scripts.Features.Audio
{
    [UsedImplicitly]
    public class AudioController : AbstractController
    {
        [Inject] private AudioModel audioModel;
        [Inject] private AudioConfig audioConfig;

        public void PlayClip(AudioEffectType effect, float delaySeconds = 0f)
        {
            var (clip, pitch) = audioConfig.GetPitchedClipTuple(effect);
            var effectData = new AudioEffectData(clip, pitch, delaySeconds);
            audioModel.PlayEffect(effectData);
        }
    }
}