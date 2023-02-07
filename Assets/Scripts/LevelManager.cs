using UnityEngine;
using UnityEngine.UI;

namespace FTT.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] private int level = 1;
        [SerializeField] private int experience = 0;
        [SerializeField] private int experienceToLevelUp = 10;
        [SerializeField] private Image experienceSlider;
        [SerializeField] private Text experienceText;
        [SerializeField] private Text levelText;

        [Header("Arrays")]
        [SerializeField] private int[] experienceArrayPerLevel;

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

        public void AddExperience(int amount)
        {
            experience += amount;
            if(experience >= experienceToLevelUp)
            {
                var diff = experienceToLevelUp - experience;
                experience = diff;
                level++;
                PlayerPrefs.SetInt("level", level);
            }
            SetExperienceValues();
            SetTextAndSlider();
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