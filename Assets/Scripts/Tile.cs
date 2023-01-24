using FTT.Farm;
using UnityEngine;

namespace FTT.Tile
{
    public class Tile
    {
        private Dirt tile;
        private bool hasDirtOn;
        private bool hasCrop;
        private Consumable.Consumable consumable;
        public Tile(Dirt tile, bool hasDirtOn, bool hasCrop)
        {
            this.tile = tile;
            this.hasDirtOn = hasDirtOn;
            this.hasCrop = hasCrop;
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
        }

        public bool HasCropOn()
        {
            return hasCrop;
        }

        public void PlantCrop(Consumable.Consumable consumable)
        {
            this.hasCrop = true;
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
    }
}