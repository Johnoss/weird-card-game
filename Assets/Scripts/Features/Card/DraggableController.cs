using Features.MVC;
using UnityEngine;

namespace Features.Card
{
    public class DraggableController : AbstractController
    {
        private readonly DraggableModel draggableModel;

        public DraggableController(DraggableModel draggableModel)
        {
            this.draggableModel = draggableModel;
        }


        public void SetStartPosition(Vector2 startPosition)
        {
            draggableModel.SetStartPosition(startPosition);
        }

        public void SetDeltaPosition(Vector2 deltaPosition)
        {
            draggableModel.SetDeltaPosition(deltaPosition);
        }
        
        public void SetDragEndPosition(Vector2 endPosition)
        {
            var totalVector = draggableModel.StartDragPosition.Value - endPosition;
            draggableModel.SetTotalDragVector(totalVector);
        }
    }
}