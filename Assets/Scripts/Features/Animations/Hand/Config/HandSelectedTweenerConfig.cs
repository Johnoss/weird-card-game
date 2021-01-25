using Assets.Scripts.Features.Animations.Config;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.Hand.Config
{
    [CreateAssetMenu(fileName = "HandSelectedTweenerConfig", menuName = "Tweener/HandSelectedTweenerConfig")]
    public class HandSelectedTweenerConfig : AbstractTweenerConfig
    {
        [Header("Positions")]
        [SerializeField] private float defaultYPosition;
        [SerializeField] private float hiddenYPosition;
        public float DefaultYPosition => defaultYPosition;
        public float HiddenYPosition => hiddenYPosition;
    }
}