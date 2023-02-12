using UnityEngine;
using FTT.Consumable;
using static FTT.Managers.LevelManager;

namespace FTT.Managers
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardGroup[] rewardsPerLevel;
        [SerializeField] private RewardItem rewardItemPrefab;
        [SerializeField] private Transform rewardParentTransform;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private GameObject shopCanvas;
        [SerializeField] private GameObject missionsCanvas;
        [SerializeField] private GameObject rewardCanvas;

        private int level;

        private void Start()
        {
            levelManager.OnLevelUp += InitReward;
        }

        private void InitReward(object sender, LevelEventArgs e)
        {
            if(rewardsPerLevel[e.level - 2].rewards.Length > 0)
            {
                for (int i = 0; i < rewardsPerLevel[e.level - 2].rewards.Length; i++)
                {
                    var cons = rewardsPerLevel[e.level - 2].rewards[i].consumableSO;
                    var amount = rewardsPerLevel[e.level - 2].rewards[i].amount;
                    level = e.level;

                    var ri = CreateRewardItems();
                    if(ri == null)
                    {
                        continue;
                    }
                    ri.InitRewardItem(cons.id, cons.icon, amount);
                }
                OpenRewardCanvas();
            }
        }

        private RewardItem CreateRewardItems()
        {
            var go = Instantiate(rewardItemPrefab, Vector3.zero, Quaternion.identity, rewardParentTransform);
            if(go.TryGetComponent(out RewardItem rewardItem))
            {
                return rewardItem;
            }
            return null;
        }

        private void OpenRewardCanvas()
        {
            gameCanvas.SetActive(false);
            shopCanvas.SetActive(false);
            missionsCanvas.SetActive(false);
            rewardCanvas.SetActive(true);
        }

        public void ClaimReward()
        {
            for (int i = 0; i < rewardsPerLevel[level - 2].rewards.Length; i++)
            {
                // If reward isn't dirt.
                if(rewardsPerLevel[level - 2].rewards[i].consumableSO.growTime > 0)
                {
                    InventoryManager.Instance.AddConsumable(rewardsPerLevel[level - 2].rewards[i].consumableSO.plant, Vector3.zero, rewardsPerLevel[level - 2].rewards[i].amount);
                }
            }
            NoThanksButton();
        }

        public void NoThanksButton()
        {
            gameCanvas.SetActive(true);
            shopCanvas.SetActive(false);
            missionsCanvas.SetActive(false);
            rewardCanvas.SetActive(false);
        }
    }

    [System.Serializable]
    public class Reward
    {
        public ConsumableSO consumableSO;
        public int amount; 
    }

    [System.Serializable]
    public class RewardGroup
    {
        public Reward[] rewards;
    }
}

