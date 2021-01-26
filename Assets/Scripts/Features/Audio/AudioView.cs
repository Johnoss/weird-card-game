using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Features.MVC;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.Scripts.Features.Audio
{
    public class AudioView : AbstractView, IInitializable
    {
        [Header("Sources")]
        [SerializeField] private List<AudioSource> audioEffectSources;
        [SerializeField] private AudioSource musicEffectSource;

        [Header("Mixers")]
        [SerializeField] private AudioMixerGroup effectsMixer;
        [SerializeField] private AudioMixerGroup musicMixer;

        [Inject] private AudioModel audioModel;

        [Inject]
        public void Initialize()
        {
            audioModel
                .OnPlayAudioEffect
                .Where(effect => effect != null)
                .Subscribe(effect => PlayEffect(effect.AudioClip, effect.Pitch, effect.DelaySeconds)).AddTo(this);
        }

        private void PlayEffect(AudioClip clip, float pitch, float effectDelaySeconds)
        {
            var audioSource = audioEffectSources.FirstOrDefault(source => !source.isPlaying);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = effectsMixer;
                audioEffectSources.Add(audioSource);
            }

            audioSource.clip = clip;
            audioSource.pitch = pitch;

            audioSource.PlayDelayed(effectDelaySeconds);
        }

        public void PlayMusic(AudioClip clip)
        {

        }
    }
}