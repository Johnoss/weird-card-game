using DG.Tweening;
using UnityEngine;

namespace Features.Animations.Config
{
    public abstract class AbstractTweenerConfig : ScriptableObject
    {
        [Header("Common Animation Definition")]
        [SerializeField] private float animationDurationSeconds = .5f;
        [SerializeField] private Ease ease = Ease.OutQuad;
        public float AnimationDurationSeconds => animationDurationSeconds;
        public Ease Ease => ease;
    }
}