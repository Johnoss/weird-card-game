using Features.MVC;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Features.Card
{
    public class DraggableModel : AbstractModel
    {
        private readonly ReactiveProperty<Vector2> startDragPosition = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<Vector2> deltaDragPosition = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<Vector2> totalDragVector = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<bool> isDragging = new ReactiveProperty<bool>();

        public IReadOnlyReactiveProperty<Vector2> StartDragPosition => startDragPosition;
        public IReadOnlyReactiveProperty<Vector2> DeltaPosition => deltaDragPosition;
        public IReadOnlyReactiveProperty<Vector2> TotalDragVector => totalDragVector;
        public IReadOnlyReactiveProperty<bool> IsDragging => isDragging;
        
        public void SetStartPosition(Vector2 startPosition)
        {
            startDragPosition.Value = startPosition;
        }

        public void SetDeltaPosition(Vector2 deltaPosition)
        {
            deltaDragPosition.Value = deltaPosition;
        }
        
        public void SetTotalDragVector(Vector2 totalVector)
        {
            totalDragVector.Value = totalVector;
        }

        public void SetIsDragging(bool isDragging)
        {
             this.isDragging.Value = isDragging;
        }
    }
}