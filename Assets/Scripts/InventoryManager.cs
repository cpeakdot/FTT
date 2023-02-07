using System.Collections.Generic;
using FTT.Controllers;
using UnityEngine;
using Internal = UnityEngine.Internal;

namespace FTT.Managers
{
    public partial class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        [SerializeField] private ParticleManager particleManager;
        private List<Inventory> InventoryItems = new();
        [SerializeField] private Transform inventory;
        [SerializeField] private InventoryItem inventoryItemPrefab;
        [SerializeField] private Consumable.Consumable testWheat;

        private List<Transform> items = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            var allConsumables = ConsumableManager.GetAllSOs();
            GetSave(allConsumables);
        }

        private void GetSave(Consumable.ConsumableSO[] allConsumables)
        {
            for (int i = 0; i < allConsumables.Length; i++)
            {
                var plantAmount = GetKey(allConsumables[i].plant);
                if (plantAmount != 0)
                {
                    GetAndAddConsumable(allConsumables[i].plant , plantAmount);
                }
            }
        }

        [ContextMenu("Add Wheat")]
        public void AddWheat()
        {
            AddConsumable(testWheat, Vector3.zero);
            ListItems();
        }

        public void AddConsumable(Consumable.Consumable consumable,[Internal.DefaultValue("Vector3.zero")] Vector3 position, int amount = 1)
        {
            var newElement = FindInventoryElement(consumable);
            if (newElement == null)
            {
                newElement = new Inventory(consumable, amount);
                InventoryItems.Add(newElement);
            }
            else
            {
                newElement.count += amount;
            }
            SaveItem(newElement.consumable , newElement.count);
            particleManager.InitParticle(ParticleManager.ParticleType.Consumable , position , Quaternion.identity, consumable.GetScriptableObject, amount);
        }

        private void GetAndAddConsumable(Consumable.Consumable consumable, int amount = 1)
        {
            var newElement = FindInventoryElement(consumable);
            if (newElement == null)
            {
                newElement = new Inventory(consumable , amount);
                InventoryItems.Add(newElement);
            }
            else
            {
                newElement.count += amount;
            }
        }

        public void RemoveConsumable(Consumable.Consumable consumable)
        {
            var element = FindInventoryElement(consumable);
            if (element == null)
            {
                return;
            }

            if (element.count > 1)
            {
                element.count -= 1;
                ReAdjustItemCount(element.consumable , element.count);
            }
            else
            {
                InventoryItems.Remove(element);
                DeleteKey(element.consumable);
            }
            
        }

        public bool TryRemoveElement(Consumable.Consumable consumable)
        {
            var element = FindInventoryElement(consumable);
            if (element != null)
            {
                if (element.count >= 1)
                {
                    RemoveConsumable(consumable);
                    return true;
                }
            }
            return false;
        }

        public Inventory FindInventoryElement(Consumable.Consumable consumable)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].consumable.GetScriptableObject == consumable.GetScriptableObject)
                {
                    return InventoryItems[i];
                }
            }
            return null;
        }

        public void ListItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                var newItem = Instantiate(inventoryItemPrefab, inventory);
                var SO = InventoryItems[i].consumable.GetScriptableObject;
                newItem.SetItem(SO.id, InventoryItems[i].count , SO.icon, SO.plant);
                items.Add(newItem.transform);
            }
        }

        public bool HasItem(Consumable.Consumable consumable, int amount)
        {
            var cons = FindInventoryElement(consumable);
            if(cons != null)
            {
                return cons.count >= amount;
            }
            return false;
        }

    }
}