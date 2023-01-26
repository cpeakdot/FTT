using System;
using FTT.Tile;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTT.Farm
{
    public class FarmingManager : MonoBehaviour
    {
        public enum SelectedObj {Hand, Seed, Water, Hoe}
        public SelectedObj selectedObj;

        [SerializeField] private Consumable.Consumable[] consumables;

        private Tile.Tile selectedTile;

        [SerializeField] private TileManager tileManager;
        [SerializeField] private GameObject seedSelection;

        private Vector3 mouseDownPos;

        private Consumable.Consumable currentCrop;

        public delegate void OnAction(SelectedObj sObj);

        public OnAction OnActionChange;

        private void Start()
        {
            selectedObj = SelectedObj.Hand;
        }

        private void Update()
        {
            TileSelection();
        }

        private void TileSelection()
        {
            if (selectedObj != SelectedObj.Hand)
                return;
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (mouseDownPos != Input.mousePosition)
                    return;
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

        public void HandPointer()
        {
            selectedObj = SelectedObj.Hand;
            OnActionChange?.Invoke(SelectedObj.Hand);
        }

        public void SeedPointer()
        {
            selectedObj = SelectedObj.Seed;
            OnActionChange?.Invoke(SelectedObj.Seed);
        }

        public void WaterPointer()
        {
            selectedObj = SelectedObj.Water;
            OnActionChange?.Invoke(SelectedObj.Water);
        }

        public void HoePointer()
        {
            selectedObj = SelectedObj.Hoe;
            OnActionChange?.Invoke(SelectedObj.Hoe);
        }

        public void PlantSeed(Consumable.Consumable plant)
        {
            currentCrop = plant;
            // var seed = Array.IndexOf(consumables, plant);
            // var seedPos = selectedTile.GetDirt().transform.position + new Vector3(.5f, 0, .5f);
            // var consSeed = Instantiate(consumables[seed], seedPos, Quaternion.identity);
            // consSeed.InitSeed();
            // consSeed.SetTile(selectedTile);
            // selectedTile.PlantCrop(consSeed);
        }

        public Consumable.Consumable GetPlant => currentCrop;
        public Consumable.Consumable[] GetConsumables => consumables;
    }
}