using Features.MVC;
using UniRx;

namespace Features.Card
{
    public class CardGesturesModel : AbstractModel
    {
        public readonly ISubject<SwipeDirection> OnSwipe = new Subject<SwipeDirection>();

        public void SetSwipe(SwipeDirection direction)
        {
            OnSwipe.OnNext(direction);
        }
    }
}