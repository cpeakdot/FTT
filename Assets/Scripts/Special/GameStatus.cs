using UnityEngine;
using System;

namespace FTT
{
    public class GameStatus : MonoBehaviour
    {
        public static GameStatus Instance;
        public static event EventHandler OnGameClosed;
        [SerializeField] private float tickTime = 3f;
        private float tickTimer = 0f;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if(tickTimer >= tickTime)
            {
                OnClosed();
                tickTimer = 0f;
            }
        }

        private void OnApplicationPause(bool pause)
        {
            OnClosed();
        }

        private void OnApplicationQuit()
        {
            OnClosed();
        }

        private void OnClosed()
        {
            OnGameClosed?.Invoke(this , EventArgs.Empty);
        }

        public void CloseApplication()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}