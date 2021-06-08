using Features.Card.Config;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Card
{
    public class DraggableView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Layout")]
        [SerializeField] private RectTransform draggableParent;

        [Header("Config")]
        [SerializeField] private GesturesConfig gesturesConfig;
        
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
            draggableParent.anchoredPosition += eventData.delta * gesturesConfig.DraggingDistanceMultiplier;
            draggableController.SetDeltaPosition(eventData.delta);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            draggableController.SetDragEndPosition(eventData.position);
        }
    }
}