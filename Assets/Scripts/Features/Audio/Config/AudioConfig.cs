using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Extensions;
using Utilities.Ranged_Float;

namespace Features.Audio.Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [Serializable]
        public class AudioEffectSetting
        {
            public AudioEffectType AudioEffect;
            public List<AudioClip> AudioClips;
            [RangedFloat(-1, 1)]
            public RangedFloat PitchOffsetRange = new RangedFloat(0,0);
        }

        [Header("Cards")]
        [SerializeField] List<AudioEffectSetting> effectSettings;

        public Tuple<AudioClip, float> GetPitchedClipTuple(AudioEffectType effect)
        {
            var setting = effectSettings.First(effectSetting => effectSetting.AudioEffect == effect);
            var clip = setting.AudioClips.RandomElement();
            var pitch = setting.PitchOffsetRange.GetRandomValue() + 1;
            return new Tuple<AudioClip, float>(clip, pitch);
        }

    }
}