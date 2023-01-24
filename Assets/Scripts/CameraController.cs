using FTT.Farm;
using UnityEngine;

namespace FTT.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public float sensitivity = 3f;
        private Vector2 touchStart;
        private const float cameraZPosition = -10f;
        [SerializeField] private float minXValue, maxXValue;
        [SerializeField] private float minYValue, maxYValue;
        [SerializeField] private FarmingManager farmingManager;

        void LateUpdate() 
        {
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
                            transform.Translate(new Vector3(-deltaX * sensitivity * Time.deltaTime, -deltaY * sensitivity * Time.deltaTime, 0));
                            /// Clamp the x and y value
                            var myPosition = transform.position;
                            float xValue = myPosition.x;
                            float yValue = myPosition.y;
                            if(minXValue != 0 || maxXValue != 0)
                                xValue = Mathf.Clamp(myPosition.x, minXValue, maxXValue);
                            if(minYValue != 0 || maxYValue != 0)
                                yValue = Mathf.Clamp(myPosition.y, minYValue, maxYValue);
                            transform.position = new Vector3(xValue, yValue, myPosition.z);
                    
                            touchStart = touch.position;
                        }
                    }
                }
            }
        }
    }
}