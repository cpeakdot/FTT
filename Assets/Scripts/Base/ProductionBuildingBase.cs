using UnityEngine;
using FTT.EventSystem;

namespace FTT.Buildings
{
    public abstract class ProductionBuildingBase : MonoBehaviour
    {
        [SerializeField] private BuildingUI.BuildingType buildingType;
        [SerializeField] private BuildingUI buildingUI;
        [SerializeField] private EventManager eventManager;
        [SerializeField] protected int emptySpaces = 1;

        private void Start() 
        {
            eventManager = EventManager.Instance;

            if(eventManager == null)
            {
                Debug.LogWarning("Event Manager is null in production building base");
            }    
        }

        public virtual void OpenUpTheUI()
        {
            buildingUI.ActivateBuildingCanvas(buildingType);
            eventManager.SendEvent(EventManager.EventType.UIActive, true);
            CreateRecipes();
        }

        protected abstract void CreateRecipes();
    }
}


