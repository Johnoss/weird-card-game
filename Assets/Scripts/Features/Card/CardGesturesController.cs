using Features.Card.Config;
using Features.MVC;
using UniRx;

namespace Features.Card
{
    public class CardGesturesController : AbstractController
    {
        private readonly DraggableModel draggableModel;
        private readonly CardGesturesModel cardGesturesModel;
        private readonly GesturesConfig gesturesConfig;

        public CardGesturesController(DraggableModel draggableModel, CardGesturesModel cardGesturesModel,
            GesturesConfig gesturesConfig)
        {
            this.draggableModel = draggableModel;
            this.cardGesturesModel = cardGesturesModel;
            this.gesturesConfig = gesturesConfig;

            draggableModel.TotalDragVector
                .Skip(1)
                .Subscribe(_ => OnSwipe());
        }

        private void OnSwipe()
        {
            var totalVector = draggableModel.TotalDragVector.Value;
            var direction = gesturesConfig.GetSwipeDirection(totalVector);
            
            cardGesturesModel.SetSwipe(direction);
        }
    }
}