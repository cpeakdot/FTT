using FTT.Buildings;
using FTT.EventSystem;
using UnityEngine;

namespace FTT.Managers
{
    public class BuildingManager : MonoBehaviour 
    {
        private Vector3 mouseDownPos;
        private const float maxRaycastDistance = 50f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (mouseDownPos != Input.mousePosition)
                    return;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out ProductionBuildingBase building))
                    {
                        building.OpenUpTheUI();
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        public void CloseTheUI()
        {
            EventManager.Instance.SendEvent(EventManager.EventType.UIActive, false);
        }
    }
}
