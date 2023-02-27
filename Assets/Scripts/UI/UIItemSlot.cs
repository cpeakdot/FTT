using FTT;
using FTT.Consumable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private BuildingOrderControllerUI orderController;
    [SerializeField] private GameObject sliderObj;
    [SerializeField] private Image fillerImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI timerText;

    public Image GetFillerImage => fillerImage;
    public Image GetIconImage => iconImage;
    public TextMeshProUGUI GetTimerText => timerText;
    public GameObject GetSliderObj => sliderObj;


    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();    
        orderController = GetComponent<BuildingOrderControllerUI>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent(out RecipeItemUI recipeItem))
            {
                print("recipeItem: " + recipeItem);
                var consumableObj = recipeItem.GetConsumableSO;
                if(consumableObj == null)
                {
                    // consumable SO is null
                    Debug.Log("consumableSO is null");
                    return;
                }
                if(orderController.TrySetOrder(consumableObj, fillerImage, timerText))
                {
                    SetItem(consumableObj);
                    eventData.pointerDrag.GetComponent<UIDragDropHandler>().OnSnappedPosition();
                }
                else{
                    Debug.Log("couldny set the order");
                }
            }
        }
    }

    public void SetItem(ConsumableSO consumableObj)
    {
        sliderObj.SetActive(true);
        this.iconImage.gameObject.SetActive(true);
        this.iconImage.sprite = consumableObj.icon;
    }

    public void ClearSlot()
    {
        fillerImage.fillAmount = 0f;
        sliderObj.SetActive(false);
        this.iconImage.sprite = null;
        this.iconImage.gameObject.SetActive(false);
    }
}
