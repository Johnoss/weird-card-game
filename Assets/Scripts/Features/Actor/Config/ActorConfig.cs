using UnityEngine;

namespace Assets.Scripts.Features.Actor.Config
{
    [CreateAssetMenu(fileName = "ActorConfig", menuName = "Config/ActorConfig")]
    public class ActorConfig : ScriptableObject
    {
        [Header("Expression Effect")]
        [SerializeField] private float effectExpressionSeconds;
        public float EffectExpressionSeconds => effectExpressionSeconds;
    }
}