using System;
using System.Collections.Generic;
using FTT.Controllers;
using UnityEngine;

namespace FTT.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        private List<Consumable.Consumable> InventoryItems = new();
        [SerializeField] private Transform inventory;
        [SerializeField] private InventoryItem inventoryItemPrefab;
        [SerializeField] private Consumable.Consumable testWheat;

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
        }

        [ContextMenu("Add Wheat")]
        public void AddWheat()
        {
            AddConsumable(testWheat);
            ListItems();
        }

        public void AddConsumable(Consumable.Consumable consumable)
        {
            InventoryItems.Add(consumable);
        }

        public void RemoveConsumable(Consumable.Consumable consumable)
        {
            InventoryItems.Remove(consumable);
        }

        public void ListItems()
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                var newItem = Instantiate(inventoryItemPrefab, inventory);
                var SO = InventoryItems[i].GetScriptableObject;
                newItem.SetItem(SO.id, SO.icon);
            }
        }
    }
}