using DG.Tweening;
using UnityEngine;

namespace Features.Animations
{
    public abstract class AbstractTweener : MonoBehaviour
    {
        protected Tween Tweener;
        
        protected void KillTweener()
        {
            Tweener?.Kill();
        }
    }
}