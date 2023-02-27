using FTT.Farm;
using UnityEngine;
using FTT.EventSystem;
using System;

namespace FTT.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public float sensitivity = 3f;
        private Vector2 touchStart;
        private const float cameraZPosition = -10f;
        [SerializeField] private float minXValue, maxXValue;
        [SerializeField] private float minZValue, maxZValue;
        [SerializeField] private FarmingManager farmingManager;
        
        private bool uiActive = false;

        private void Start() 
        {
            EventManager.OnUIToggle += UIEvent;
        }

        void LateUpdate() 
        {
            if(uiActive)
                return;
            if (farmingManager != null)
            {
                if (farmingManager.selectedObj == FarmingManager.SelectedObj.Hand)
                {
                    if (Input.touchCount > 0) 
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began) 
                        {
                            touchStart = touch.position;
                        } 
                        else if (touch.phase == TouchPhase.Moved) 
                        {
                            float deltaX = touch.position.x - touchStart.x;
                            float deltaY = touch.position.y - touchStart.y;
                            transform.Translate(new Vector3(-deltaX * sensitivity * Time.deltaTime, 0, -deltaY * sensitivity * Time.deltaTime));
                            /// Clamp the x and y value
                            var myPosition = transform.position;
                            float xValue = myPosition.x;
                            float zValue = myPosition.z;
                            if(minXValue != 0 || maxXValue != 0)
                                xValue = Mathf.Clamp(myPosition.x, minXValue, maxXValue);
                            if(minZValue != 0 || maxZValue != 0)
                                zValue = Mathf.Clamp(myPosition.z, minZValue, maxZValue);
                            transform.position = new Vector3(xValue, myPosition.y, zValue);
                    
                            touchStart = touch.position;
                        }
                    }
                }
            }
        }

        private void UIEvent(object sender, EventManager.OnUIToggleEventArgs e)
        {
            uiActive = e.isUIOn;
        }
    }
}