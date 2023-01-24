using System;
using FTT.Tile;
using UnityEngine;

namespace FTT.Farm
{
    public class FarmingManager : MonoBehaviour
    {
        public enum SelectedObj {Dirt, Water}
        public SelectedObj selectedObj;

        [SerializeField] private Consumable.Consumable[] consumables;

        private Tile.Tile selectedTile;

        [SerializeField] private TileManager tileManager;
        [SerializeField] private GameObject seedSelection;

        private void Start()
        {
            selectedObj = SelectedObj.Dirt;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
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
                            selectedTile = tile;
                            seedSelection.SetActive(true);
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
        
        private void SelectObj(SelectedObj sObj)
        {
            selectedObj = sObj;
        }

        public void PlantSeed(Consumable.Consumable plant)
        {
            selectedTile.PlantCrop();
            var seed = Array.IndexOf(consumables, plant);
            var seedPos = selectedTile.GetDirt().transform.position + new Vector3(.5f, 0, .5f);
            var consSeed = Instantiate(consumables[seed], seedPos, Quaternion.identity);
            consSeed.InitSeed();
        }
    }
}