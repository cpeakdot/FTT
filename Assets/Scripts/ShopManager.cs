using FTT.Consumable;
using UnityEngine;

namespace FTT.Managers
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance;
        [SerializeField] private GameObject shopCanvas;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private GameObject missionsCanvas;
        [SerializeField] private GameObject rewardCanvas;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InventoryManager inventoryManager;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            shopCanvas.SetActive(false);
        }

        public void OpenShop()
        {
            shopCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            missionsCanvas.SetActive(false);
            rewardCanvas.SetActive(false);
        }

        public void BuyItem(ConsumableSO consumableSO, int amount = 1)
        {
            var cost = consumableSO.marketPrice;
            if (gameManager.TrySpendGold(cost))
            {
                inventoryManager.AddConsumable(consumableSO.plant, Vector3.zero, amount);
                StoredItems.TryAddItemToAchievements(consumableSO);
            }
            else
            {
                // Not enough money!
            }
        }
    }
}

