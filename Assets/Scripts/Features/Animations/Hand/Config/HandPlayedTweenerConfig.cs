using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Hand.Config
{
    [CreateAssetMenu(fileName = "HandPlayedTweenerConfig", menuName = "Tweener/HandPlayedTweenerConfig")]
    public class HandPlayedTweenerConfig : AbstractTweenerConfig
    {
        [Header("Positions")]
        [SerializeField] private float hideYPosition;
        public float HideYPosition => hideYPosition;
    }
}