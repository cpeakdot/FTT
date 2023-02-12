using UnityEngine;
using UnityEngine.EventSystems;

namespace FTT
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIDragDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Vector3 initialPosition;
        private bool canBeMoved = false;

        private void Awake() 
        {
            rectTransform = GetComponent<RectTransform>();    
            canvasGroup = GetComponent<CanvasGroup>();
            canBeMoved = false;
        }

        private void Start() 
        {
            initialPosition = rectTransform.anchoredPosition;    
        }

        public void CanMove(Canvas canvas)
        {
            canBeMoved = true;
            this.canvas = canvas;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(canvas != null)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            rectTransform.anchoredPosition = initialPosition;
        }

        public void OnSnappedPosition()
        {

        }
    }
}


