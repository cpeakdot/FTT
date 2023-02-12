using System;
using UnityEngine;

namespace FTT.EventSystem
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private void Awake() 
        {
            if(Instance == null)
            {
                Instance = this;
            }    
            else
            {
                Debug.Log("There are multiple EventManagers");
                Destroy(this.gameObject);
            }
        }

        public static event EventHandler<OnUIToggleEventArgs> OnUIToggle;
        public class OnUIToggleEventArgs : EventArgs
        {
            public bool isUIOn { get; set;}
        }

        public enum EventType
        {
            UIActive,
        }

        public void SendEvent(EventType eventType, bool value = true)
        {
            switch (eventType)
            {
                case EventType.UIActive:

                    var eArgs = new OnUIToggleEventArgs();
                    eArgs.isUIOn = value;
                    OnUIToggle?.Invoke(this, eArgs);

                    break;
                default:
                    break;
            }
        }
    }
}


