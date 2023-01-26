using System;
using cpeak.cPool;
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

        public void InitSeed()
        {
            harvestable = false;
            growTimer = 0f;
            
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
    }
}