using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Card
{
    public class DraggableView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DraggableController draggableController;

        public void Setup(DraggableController draggableController)
        {
            this.draggableController = draggableController;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            draggableController.SetStartPosition(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            draggableController.SetDeltaPosition(eventData.delta);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            draggableController.SetDragEndPosition(eventData.position);
        }
    }
}