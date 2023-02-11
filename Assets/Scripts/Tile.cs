using FTT.Farm;
using UnityEngine;

namespace FTT.Tile
{
    [System.Serializable]
    public class Tile
    {
        private Dirt tile;
        private bool hasDirtOn;
        private bool hasCrop;
        private Consumable.Consumable consumable;
        private int xPosition;
        private int zPosition;
        private float timer;
        public Tile(Dirt tile, bool hasDirtOn, bool hasCrop, int xPosition, int zPosition)
        {
            this.tile = tile;
            this.hasDirtOn = hasDirtOn;
            this.hasCrop = hasCrop;
            this.xPosition = xPosition;
            this.zPosition = zPosition;
        }

        public bool HasDirt()
        {
            return hasDirtOn;
        }

        public Dirt GetDirt()
        {
            return tile;
        }

        public void HarvestCrop()
        {
            this.hasCrop = false;
            PlayerPrefs.SetInt("tile" + xPosition + zPosition , 0);
        }

        public bool HasCropOn()
        {
            return hasCrop;
        }

        public void PlantCrop(Consumable.Consumable consumable)
        {
            this.hasCrop = true;
            PlayerPrefs.SetInt("tile" + xPosition + zPosition , 1);
            this.consumable = consumable;
        }

        public void SetCrop(Consumable.Consumable consumable)
        {
            this.consumable = consumable;
        }

        public Consumable.Consumable GetCrop()
        {
            if (this.hasCrop)
            {
                return consumable;
            }
            else
            {
                return null;
            }
        }

        public Vector3 GetWorldPosition()
        {
            return new Vector3(xPosition , 0 , zPosition);
        }

        public void SaveTile(int consumableIndex, float timer)
        {
            this.timer = timer;
            PlayerPrefs.SetInt("xPosition" + xPosition + "zPosition" + zPosition , consumableIndex);
            PlayerPrefs.SetFloat(xPosition + zPosition + "timer" , timer);
        }

        public float GetTimer()
        {
            return PlayerPrefs.GetFloat(xPosition + zPosition + "timer" , 0f);
        }

        public int GetConsumableIndex()
        {
            return PlayerPrefs.GetInt("xPosition" + xPosition + "zPosition" + zPosition , -1);
        }
    }
}