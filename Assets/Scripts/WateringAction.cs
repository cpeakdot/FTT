using FTT.Farm;
using FTT.Tile;
using UnityEngine;
using cpeak.cPool;
using FTT.Consumable;

namespace FTT.Actions
{
    public class WateringAction : FarmAction
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private int experienceAmount;
        private bool wateringAction = false;
        private cPool pool;

        public override void IncreaseExperience(ConsumableSO consumable = null)
        {
            var exp = consumable == null ? experienceAmount : consumable.experience;
            levelManager.AddExperience(experienceAmount);
        }

        private void Start()
        {
            pool = cPool.instance;
            farmingManager.OnActionChange += (FarmingManager.SelectedObj selectedObj) =>
            {
                wateringAction = selectedObj == FarmingManager.SelectedObj.Water;
            };
        }

        private void Update()
        {
            if (wateringAction)
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
                                /// Tile is null
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
                                WaterCrop(tile);
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

        private void WaterCrop(Tile.Tile tile)
        {
            var crop = tile.GetCrop();
            
            if (crop.IsGrowing || crop.IsHarvestable)
                return;
            
            pool.GetPoolObject("waterSplash", crop.transform.position, Quaternion.identity, true, 2f);
            crop.Water();
            IncreaseExperience();
        }
    }
}