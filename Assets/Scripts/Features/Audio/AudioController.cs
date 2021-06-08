using System;
using Features.Audio.Config;
using Features.MVC;
using JetBrains.Annotations;
using Zenject;

namespace Features.Audio
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