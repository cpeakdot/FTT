using System;
using FTT.Consumable;
using FTT.Farm;
using FTT.Managers;
using FTT.Tile;
using UnityEngine;

namespace FTT.Actions
{
    public class PlantingAction : FarmAction
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private int experienceAmount = 1;

        private bool planting = false;
        
        private void Start()
        {
            farmingManager.OnActionChange += (FarmingManager.SelectedObj selectedObj) =>
            {
                planting = selectedObj == FarmingManager.SelectedObj.Seed;
            };
        }
        
        private void Update()
        {
            if (planting)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance))
                    {
                        Debug.Log(hitInfo.transform.name, hitInfo.transform.gameObject);
                        if (hitInfo.transform.TryGetComponent(out Dirt dirt))
                        {
                            var tile = tileManager.GetTile(dirt);
                            if (!tile.HasCropOn())
                            {
                                var consumable = farmingManager.GetPlant;
                                
                                if(consumable == null)
                                    return;
                                
                                PlantCrop(tile, consumable);
                                //seedSelection.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Has Crop On", tile.GetDirt().gameObject);
                            }
                        }
                        else
                        {
                            Debug.Log("Is not dirt!");
                        }
                    }
                }
            }
        }

        public void PlantCrop(Tile.Tile targetTile , Consumable.Consumable targetConsumable , bool watered = false, float timer = 0f, bool overdrive = false)
        {
            print(targetTile + " " + targetConsumable + " " + watered + " " + timer);
            var consumables = farmingManager.GetConsumables;
            
            if (targetTile.HasCropOn() && !overdrive)
            {
                return;
            }
            
            if (!inventoryManager.TryRemoveElement(targetConsumable) && !overdrive)
            {
                farmingManager.HandPointer();
                return;
            }
            
            var seed = Array.IndexOf(consumables, targetConsumable);
            var seedPos = targetTile.GetDirt().transform.position + new Vector3(.5f, 0, .5f);
            var consSeed = Instantiate(consumables[seed], seedPos, Quaternion.identity);
            consSeed.InitSeed(watered, timer);
            consSeed.SetTile(targetTile);
            targetTile.PlantCrop(consSeed);
            IncreaseExperience();
        }

        public override void IncreaseExperience(ConsumableSO consumable = null)
        {
            var exp = consumable == null ? experienceAmount : consumable.experience;
            levelManager.AddExperience(exp);
        }
    }
}