using FTT.Consumable;
using UnityEngine;

namespace FTT.Managers
{
    public class ConsumableManager : MonoBehaviour
    {
        [SerializeField] private ConsumableSO[] consumables;
        private static ConsumableSO[] consumableSOs;

        private void Awake()
        {
            consumableSOs = consumables;
        }

        public static ConsumableSO GetConsumableSO(int index)
        {
            return consumableSOs[index];
        }

        public static ConsumableSO[] GetAllSOs()
        {
            return consumableSOs;
        }

        public static int GetConsumableIndex(ConsumableSO consumableSO)
        {
            for (int i = 0; i < consumableSOs.Length; i++)
            {
                if(consumableSOs[i] == consumableSO)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

