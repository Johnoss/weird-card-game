using Features.MVC;
using JetBrains.Annotations;
using UniRx;

namespace Features.Audio
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