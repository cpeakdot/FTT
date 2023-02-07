using UnityEngine.UI;
using UnityEngine;
using FTT.Consumable;
using FTT.Managers;
using System;

public class MissionItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private Text title;
    [SerializeField] private Text rewardAmount;
    [SerializeField] private int reward;
    [SerializeField] private int count = 1;
    [SerializeField] private ConsumableSO consumable;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetUI(Sprite icon, string title, string rewardAmount,int count, ConsumableSO consumable)
    {
        this.icon.sprite = icon;
        this.title.text = title.ToUpper();
        this.rewardAmount.text = rewardAmount;
        this.reward = Convert.ToInt32(rewardAmount);
        this.consumable = consumable;
        this.count = count;
    }

    private void OnClick()
    {
        TryClaimMission();
    }

    /// <summary>
    ///     Button action.
    /// </summary>
    private void TryClaimMission()
    {
        if(CanBeClaimed())
        {
            ClaimMission();
            GameManager.Instance.AdjustGold(reward);
            Destroy(this.gameObject);
        }
    }

    private void ClaimMission()
    {
        if (consumable.ingredients.Length == 0)
        {
            for (int i = 0; i < count; i++)
            {
                InventoryManager.Instance.RemoveConsumable(consumable.plant);
            }
        }
        else
        {
            for (int i = 0; i < consumable.ingredients.Length; i++)
            {
                for (int j = 0; j < consumable.ingredients[i].count; j++)
                {
                    for (int z = 0; z < count; z++)
                    {
                        InventoryManager.Instance.RemoveConsumable(consumable.ingredients[i].consumable.plant);
                    }
                }
            }
        }
    }

    private bool CanBeClaimed()
    {
        if (consumable.ingredients.Length == 0)
        {
            var removable = InventoryManager.Instance.HasItem(consumable.plant , count);
            return removable;
        }
        else
        {
            for (int i = 0; i < consumable.ingredients.Length; i++)
            {
                var removable = InventoryManager.Instance.HasItem(consumable.plant , count);
                if(!removable)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
