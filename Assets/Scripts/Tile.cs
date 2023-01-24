using FTT.Farm;
using UnityEngine;

namespace FTT.Tile
{
    public class Tile
    {
        private Dirt tile;
        private bool hasDirtOn;
        private bool hasCrop;
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

        public void PlantCrop()
        {
            this.hasCrop = true;
        }
    }
}