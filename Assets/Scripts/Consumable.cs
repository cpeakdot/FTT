using System;
using cpeak.cPool;
using FTT.Managers;
using UnityEngine;

namespace FTT.Consumable
{
    public class Consumable : MonoBehaviour
    {
        [SerializeField] private ConsumableSO scriptableObject;
        [SerializeField] private Transform visual;
        private float growTime;
        private float growTimer = 0f;
        private bool growing = false;
        private bool harvestable;
        private Tile.Tile tile;
        private cPool pool;

        private void Awake()
        {
            growTime = scriptableObject.growTime;
        }

        private void Start()
        {
            pool = cPool.instance;
            GameStatus.OnGameClosed += GameStatus_OnGameClosed;
        }

        private void Update()
        {
            if (growing)
            {
                growTimer += Time.deltaTime;
                if (growTimer >= growTime)
                {
                    harvestable = true;
                    growing = false;
                }
                var visualScale = visual.transform.localScale;
                visualScale.y = Mathf.Lerp(.1f, 1f, growTimer / growTime);
                visual.transform.localScale = visualScale;
            }
        }

        public void InitSeed(bool watered = false, float timer = 0f)
        {
            harvestable = false;
            growTimer = timer;
            growing = watered;
            
            var visualScale = visual.transform.localScale;
            visualScale.y = .1f;
            visual.transform.localScale = visualScale;
        }

        public void Water()
        {
            growing = true;
            Debug.Log("Watered " + "Growing: " + growing, this.gameObject);
        }

        public void Harvest()
        {
            if (!harvestable || tile == null)
                return;
            
            tile.HarvestCrop();
            this.tile = null;
            pool.ReleaseObject(scriptableObject.id, this.gameObject);
        }

        public bool IsGrowing => growing;
        public bool IsHarvestable => harvestable;
        public ConsumableSO GetScriptableObject => scriptableObject;

        public void SetTile(Tile.Tile targetTile)
        {
            tile = targetTile;
        }

        private void GameStatus_OnGameClosed(object sender , EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var consumableIndex = GetConsumableIndex(this);
            if(this.tile != null)
            {
                this.tile.SaveTile(consumableIndex , growTimer);
            }
        }
        
        private int GetConsumableIndex(Consumable consumable)
        {
            return ConsumableManager.GetConsumableIndex(this.scriptableObject);
        }

        [ContextMenu("Test")]
        private void Testing()
        {
            Debug.Log("tile: " + this.tile + " worldPos: " + tile.GetWorldPosition() + " crop: " + this.tile.GetCrop(), tile.GetDirt().gameObject);
        }
    }
}