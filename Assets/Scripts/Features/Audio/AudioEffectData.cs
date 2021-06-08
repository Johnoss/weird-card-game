using UnityEngine;

namespace Features.Audio
{
    public class AudioEffectData
    {
        public AudioClip AudioClip { get; }
        public float Pitch { get; }
        public float DelaySeconds { get; }

        public AudioEffectData(AudioClip audioClip, float pitch, float delaySeconds)
        {
            AudioClip = audioClip;
            Pitch = pitch;
            DelaySeconds = delaySeconds;
        }
    }
}