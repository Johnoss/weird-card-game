using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Card.Config
{
    [CreateAssetMenu(fileName = "TranslateToParentTweenerConfig", menuName = "Tweener/TranslateToParentTweenerConfig")]
    public class TranslateToParentTweenerConfig : AbstractTweenerConfig
    {
        [Header("Positions")]
        [SerializeField] private float suspendSelectedSeconds;
        public float SuspendSelectedSeconds => suspendSelectedSeconds;

    }
}