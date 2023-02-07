using UnityEngine;

namespace FTT.Consumable
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/ConsumableSO", order = 1)]
    public class ConsumableSO : ScriptableObject
    {
        public Consumable plant;
        public float growTime;
        public string id;
        public Sprite icon;
        public int marketPrice;
        public int marketMissionOrderMaxAmount = 1;
        public int sellPrice = 10;
        public int experience = 1;
        public Ingredient[] ingredients;
    }

    [System.Serializable]
    public class Ingredient
    {
        public ConsumableSO consumable;
        public int count = 1;
    }
}