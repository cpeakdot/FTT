namespace FTT
{
    [System.Serializable]
    public class Inventory
    {
        public Consumable.Consumable consumable;
        public int count;

        public Inventory(Consumable.Consumable consumable, int count)
        {
            this.consumable = consumable;
            this.count = count;
        }

        public int GetCount()
        {
            return count;
        }

        public Consumable.Consumable GetPlant()
        {
            return consumable;
        }
    }
}