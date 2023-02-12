using UnityEngine.UI;
using UnityEngine;

namespace FTT
{
    public class RewardItem : MonoBehaviour
    {
        [SerializeField] private Text rewardText;
        [SerializeField] private Text rewardAmountText;
        [SerializeField] private Image rewardIcon;
        [SerializeField] private int rewardAmount;

        public void InitRewardItem(string rewardText, Sprite rewardIcon, int rewardAmount)
        {
            this.rewardText.text = rewardText;
            this.rewardIcon.sprite = rewardIcon;
            this.rewardAmount = rewardAmount;
            this.rewardAmountText.text = "+" + rewardAmount;
        }
    }
}

