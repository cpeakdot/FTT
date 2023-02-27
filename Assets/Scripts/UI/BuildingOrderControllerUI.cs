using System;
using FTT;
using FTT.Consumable;
using FTT.Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(UIItemSlot))]
public class BuildingOrderControllerUI : MonoBehaviour
{
    [SerializeField] private ConsumableSO consumable;
    [SerializeField] private UIItemSlot uiItemSlot;
    [SerializeField] private int orderControllerPrefIndex = 1;
    [SerializeField] private ConsumableManager consumableManager;
    private float timer = 0f;
    private bool completed = false;
    private Image fillImage;
    private TextMeshProUGUI timerText;

    private void Start() 
    {
        GetSave();
        GameStatus.OnGameClosed += SaveProgress;
    }

    public bool TrySetOrder(ConsumableSO consumable, Image fillImage, TextMeshProUGUI leftTimerText)
    {
        if(!TryRemoveIngredients(consumable))
        {
            Debug.Log("Not enough ingredient!");
            return false;
        }
        if(this.consumable == null)
        {
            SetOrder(consumable);
            this.fillImage = fillImage;
            this.timerText = leftTimerText;
            return true;
        }
        return false;
    }

    private bool TryRemoveIngredients(ConsumableSO consumable)
    {
        for (int i = 0; i < consumable.ingredients.Length; i++)
        {
            if(!InventoryManager.Instance.HasItem(consumable.ingredients[i].consumable.plant, consumable.ingredients[i].count))
            {
                return false;
            }
        }
        RemoveIngredients(consumable);
        return true;
    }

    private void RemoveIngredients(ConsumableSO consumable)
    {
        for (int i = 0; i < consumable.ingredients.Length; i++)
        {
            for (int j = 0; j < consumable.ingredients[i].count; j++)
            {
                InventoryManager.Instance.TryRemoveElement(consumable.ingredients[i].consumable.plant);
            }
        }
    }
    
    private void SetOrder(ConsumableSO consumable)
    {
        this.consumable = consumable;
    }

    private void Update()
    {
        if(consumable != null && !completed)
        {
            timer += Time.deltaTime;

            if(fillImage != null)
            {
                fillImage.fillAmount = timer / consumable.growTime;
            }

            if(timerText != null)
            {
                timerText.text = (int)(consumable.growTime - timer) + " sec";
            }

            if(timer >= consumable.growTime)
            {
                completed = true;
            }
        }
    }

    public void RecieveConsumable()
    {
        if(completed && consumable != null)
        {
            InventoryManager.Instance.AddConsumable(consumable.plant, Vector3.zero, 1);
            consumable = null;
            timer = 0f;
            uiItemSlot.ClearSlot();
        }
    }

    private void GetSave()
    {
        var hasConsumable = PlayerPrefs.GetInt("orderControllerPref" + orderControllerPrefIndex, 0) == 0 ? false : true;
        if(hasConsumable)
        {
            this.fillImage = uiItemSlot.GetFillerImage;
            this.timerText = uiItemSlot.GetTimerText;

            var cons = ConsumableManager.GetConsumableSO(PlayerPrefs.GetInt("orderControllerConsumablePref" + orderControllerPrefIndex, 0));
            this.consumable = cons;
            this.timer = PlayerPrefs.GetFloat("orderControllerTimer" + orderControllerPrefIndex, 0f);
            uiItemSlot.SetItem(consumable);
        }
    }

    private void SaveProgress(object sender, EventArgs eventArgs)
    {
        PlayerPrefs.SetInt("orderControllerPref" + orderControllerPrefIndex, consumable == null ? 0 : 1);
        Debug.Log("Save Order " + consumable);
        if(consumable != null)
        {
            var consumableIndex = ConsumableManager.GetConsumableIndex(consumable);
            PlayerPrefs.SetInt("orderControllerConsumablePref" + orderControllerPrefIndex, consumableIndex);
            PlayerPrefs.SetFloat("orderControllerTimer" + orderControllerPrefIndex, timer);
        }
    }
}
