using FTT.Consumable;
using FTT.Tile;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTT.Farm
{
    public class FarmingManager : FarmAction
    {
        public static FarmingManager Instance;
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

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            selectedObj = SelectedObj.Hand;
            OnActionChange?.Invoke(selectedObj);
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
                if (Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance))
                {
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
                            /// Has crop on
                        }
                    }
                    else
                    {
                        /// Its not dirt
                    }
                }
            }
        }

        public void HandPointer()
        {
            selectedObj = SelectedObj.Hand;
            OnActionChange?.Invoke(SelectedObj.Hand);
            CloseSeedSelection();
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
            CloseSeedSelection();
        }

        public void HoePointer()
        {
            selectedObj = SelectedObj.Hoe;
            OnActionChange?.Invoke(SelectedObj.Hoe);
            CloseSeedSelection();
        }

        private void CloseSeedSelection()
        {
            seedSelection.SetActive(false);
        }

        public Consumable.Consumable GetPlant => currentCrop;

        public void SetPlant(Consumable.Consumable plant)
        {
            CloseSeedSelection();
            currentCrop = plant;
        }

        public override void IncreaseExperience(ConsumableSO consumable = null)
        {
        }

        public Consumable.Consumable[] GetConsumables => consumables;
    }
}