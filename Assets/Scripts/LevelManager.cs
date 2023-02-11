using FTT.Tile;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace FTT.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private TileManager tileManager;
        [SerializeField] private int level = 1;
        [SerializeField] private int experience = 0;
        [SerializeField] private int experienceToLevelUp = 10;
        [SerializeField] private Image experienceSlider;
        [SerializeField] private Text experienceText;
        [SerializeField] private Text levelText;

        [Header("Arrays")]
        [SerializeField] private int[] experienceArrayPerLevel;

        public event EventHandler<LevelEventArgs> OnLevelUp;

        public class LevelEventArgs : EventArgs
        {
            public int level {get; set;}
        }

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

            SetExperienceValues();
            level = PlayerPrefs.GetInt("level", 1);
            experience = PlayerPrefs.GetInt("levelExp", 0);
            SetTextAndSlider();
        }

        private void Start()
        {
            tileManager = TileManager.Instance;
            if(tileManager == null)
            {
                Debug.LogWarning("Tile Manager is Null!" , this);
            }
            tileManager.InitTiles(level);
        }

        public void AddExperience(int amount)
        {
            experience += amount;
            LevelUp();
            SetExperienceValues();
            SetTextAndSlider();
        }

        private void LevelUp()
        {
            if (experience >= experienceToLevelUp)
            {
                var diff = experienceToLevelUp - experience;
                experience = diff;
                level++;
                PlayerPrefs.SetInt("level" , level);

                var args = new LevelEventArgs();
                args.level = level;
                OnLevelUp?.Invoke(this, args);
            }

            print("level up");
            /// Sets up dirt according to level
            tileManager.LevelUpDirtExpansion(level);
        }

        private void SetTextAndSlider()
        {
            levelText.text = level.ToString();
            experienceText.text = "%" + ((experience * 100) / experienceToLevelUp);
            experienceSlider.fillAmount = ((float)experience / experienceToLevelUp);
        }

        private void SetExperienceValues()
        {
            if(level >= experienceArrayPerLevel.Length)
            {
                experienceToLevelUp = experienceArrayPerLevel[^1];
            }
            else
            {
                experienceToLevelUp = experienceArrayPerLevel[level];
            }
        }
    }
}