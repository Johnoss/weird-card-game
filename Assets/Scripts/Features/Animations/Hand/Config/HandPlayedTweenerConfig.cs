using Features.Animations.Config;
using UnityEngine;

namespace Features.Animations.Hand.Config
{
    [CreateAssetMenu(fileName = "HandPlayedTweenerConfig", menuName = "Tweener/HandPlayedTweenerConfig")]
    public class HandPlayedTweenerConfig : AbstractTweenerConfig
    {
        [Header("Positions")]
        [SerializeField] private float hideYPosition;
        public float HideYPosition => hideYPosition;
    }
}