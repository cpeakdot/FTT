using System;
using cpeak.cPool;
using FTT.Consumable;
using FTT.Farm;
using FTT.Managers;
using FTT.Tile;
using UnityEngine;

namespace FTT.Actions
{
    public class HarvestAction : FarmAction
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private int experience;
        private const int harvestSeedAmount = 2;
        private bool harvesting = false;
        private cPool pool;

        private void Start()
        {
            pool = cPool.instance;
            farmingManager.OnActionChange += (FarmingManager.SelectedObj selectedObj) =>
            {
                harvesting = selectedObj == FarmingManager.SelectedObj.Hoe;
            };
        }
        
        private void Update()
        {
            if (harvesting)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance))
                    {
                        if (hitInfo.transform.TryGetComponent(out Dirt dirt))
                        {
                            var tile = tileManager.GetTile(dirt);
                            if (tile == null)
                            {
                                Debug.Log("Tile is null!");
                                return;
                            }
                            if (tile.HasCropOn())
                            {
                                var crop = tile.GetCrop();
                                if (crop == null)
                                {
                                    /// Crop is null
                                    return;
                                }
                                HarvestCrop(tile);
                            }
                        }
                        else
                        {
                            /// Its not dirt
                        }
                    }
                }
            }
        }

        private void HarvestCrop(Tile.Tile tile)
        {
            var crop = tile.GetCrop();
            if (crop == null || !crop.IsHarvestable)
                return;
            inventoryManager.AddConsumable(crop, crop.transform.position, harvestSeedAmount);
            pool.GetPoolObject("harvestingEffect", crop.transform.position, Quaternion.identity, true, 3f);
            crop.Harvest();
            IncreaseExperience(crop.GetScriptableObject);
        }

        public override void IncreaseExperience(ConsumableSO consumable = null)
        {
            var exp = consumable == null ? experience : consumable.experience;
            levelManager.AddExperience(exp);
        }
    }
}