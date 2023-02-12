using FTT;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();    
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.anchoredPosition = this.rectTransform.anchoredPosition;
                eventData.pointerDrag.GetComponent<UIDragDropHandler>().OnSnappedPosition();
            }
        }
    }
}
