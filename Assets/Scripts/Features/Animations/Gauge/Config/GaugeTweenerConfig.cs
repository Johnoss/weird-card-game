using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Gauge.Config
{
    [CreateAssetMenu(fileName = "GaugeTweenerConfig", menuName = "Tweener/GaugeTweenerConfig")]
    public class GaugeTweenerConfig : AbstractTweenerConfig
    {
        [Header("Gauge")]
        [SerializeField] private float shakeStrength;
        public float ShakeStrength => shakeStrength;
        [SerializeField] private Vector3 defaultScale;
        public Vector3 DefaultScale => defaultScale;
    }
}