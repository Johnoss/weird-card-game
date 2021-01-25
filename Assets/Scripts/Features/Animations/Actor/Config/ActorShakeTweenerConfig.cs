using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Actor.Config
{
    [CreateAssetMenu(fileName = "ActorShakeTweenerConfig", menuName = "Tweener/ActorShakeTweenerConfig")]
    public class ActorShakeTweenerConfig : AbstractTweenerConfig
    {
        [Header("Shake")]
        [SerializeField] private float shakeStrength;
        [SerializeField] private int vibrato;
        [SerializeField] private float randomness;
        public Vector3 ShakeStrength => Vector3.one * shakeStrength;
        public int Vibrato => vibrato;
        public float Randomness => randomness;
    }
}