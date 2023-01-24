using UnityEngine;

namespace FTT.Consumable
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/ConsumableSO", order = 1)]
    public class ConsumableSO : ScriptableObject
    {
        public float growTime;
    }
}