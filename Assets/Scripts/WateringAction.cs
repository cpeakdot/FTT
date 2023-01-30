using FTT.Farm;
using FTT.Tile;
using UnityEngine;
using cpeak.cPool;

namespace FTT.Actions
{
    public class WateringAction : MonoBehaviour
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        private bool wateringAction = false;
        private cPool pool;

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
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, 10f))
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
                                    Debug.Log("crop is null!");
                                    return;
                                }
                                WaterCrop(tile);
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

        private void WaterCrop(Tile.Tile tile)
        {
            var crop = tile.GetCrop();
            
            if (crop.IsGrowing || crop.IsHarvestable)
                return;
            
            pool.GetPoolObject("waterSplash", crop.transform.position, Quaternion.identity, true, 2f);
            crop.Water();
        }
    }
}