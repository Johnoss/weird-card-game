using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Card.Config
{
    [CreateAssetMenu(fileName = "PlayCardTweenerConfig", menuName = "Tweener/PlayCardTweenerConfig")]
    public class PlayCardTweenerConfig : AbstractTweenerConfig
    {
        [Header("Throw Card")]
        [SerializeField] private Vector2 targetOffset;
        [RangedFloat(-360, 360)]
        [SerializeField] private RangedFloat targetZRotationRange;
        public Vector2 TargetOffset => targetOffset;
        public Vector3 TargetRotateBy => Vector3.forward * targetZRotationRange.GetRandomValue();
    }
}