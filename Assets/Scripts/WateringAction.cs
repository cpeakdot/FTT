using System;
using FTT.Farm;
using FTT.Tile;
using UnityEngine;

namespace FTT.Actions
{
    public class WateringAction : MonoBehaviour
    {
        [SerializeField] private FarmingManager farmingManager;
        [SerializeField] private TileManager tileManager;
        private bool wateringAction = false;

        private void Start()
        {
            farmingManager.OnActionChange += (FarmingManager.SelectedObj selectedObj) =>
            {
                wateringAction = selectedObj == FarmingManager.SelectedObj.Water;
            };
        }

        private void Update()
        {
            if (wateringAction)
            {
                if (Input.GetMouseButtonDown(0))
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
                                tile.GetCrop().Water();
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
    }
}