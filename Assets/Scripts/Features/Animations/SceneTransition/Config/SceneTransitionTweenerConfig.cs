using Assets.Scripts.Features.Animations.Config;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Features.Animations.SceneTransition.Config
{
    [CreateAssetMenu(fileName = "SceneTransitionTweenerConfig", menuName = "Tweener/SceneTransitionTweenerConfig")]
    public class SceneTransitionTweenerConfig : AbstractTweenerConfig
    {
        [Header("Fade Out")]
        [SerializeField] private Ease fadeOutEase;
        public Ease FadeOutEase => fadeOutEase;
    }
}