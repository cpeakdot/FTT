using UnityEngine;
using FTT.Managers;
using FTT.Consumable;

namespace FTT
{
    public abstract class FarmAction : MonoBehaviour
    {
        protected LevelManager levelManager;
        protected int maxRaycastDistance = 100;
        public abstract void IncreaseExperience(ConsumableSO consumable = null);

        public void Awake()
        {
            levelManager = LevelManager.Instance;
        }
    }
}