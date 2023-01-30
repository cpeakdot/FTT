using System;
using FTT.Farm;
using FTT.Managers;
using FTT.Tile;
using UnityEngine;

namespace FTT.Actions
{
    public class PlantingAction : MonoBehaviour
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private InventoryManager inventoryManager;

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
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, 10f))
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

        private void PlantCrop(Tile.Tile targetTile, Consumable.Consumable targetConsumable)
        {
            var consumables = farmingManager.GetConsumables;
            
            if (targetTile.HasCropOn())
            {
                return;
            }
            
            if (!inventoryManager.TryRemoveElement(targetConsumable))
            {
                return;
            }
            
            var seed = Array.IndexOf(consumables, targetConsumable);
            var seedPos = targetTile.GetDirt().transform.position + new Vector3(.5f, 0, .5f);
            var consSeed = Instantiate(consumables[seed], seedPos, Quaternion.identity);
            consSeed.InitSeed();
            consSeed.SetTile(targetTile);
            targetTile.PlantCrop(consSeed);
        }
    }
}