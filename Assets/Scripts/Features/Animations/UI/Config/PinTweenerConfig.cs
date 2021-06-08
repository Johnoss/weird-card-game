using Features.Animations.Config;
using UnityEngine;

namespace Features.Animations.UI.Config
{
    [CreateAssetMenu(fileName = "PinTweenerConfig", menuName = "Tweener/PinTweenerConfig")]
    public class PinTweenerConfig : AbstractTweenerConfig
    {
        [Header("Pin")]
        [SerializeField] private Vector2 pinHiddenAnchorPosition;
        public Vector2 PinHiddenAnchorPosition => pinHiddenAnchorPosition;
    }
}