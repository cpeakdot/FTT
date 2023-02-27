using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTT.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;

        [Header("Money")]
        [SerializeField] private Text[] goldTextArray;

        private void Start()
        {
            if(gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            gameManager.OnGoldAdjusted += (amount) =>
            {
                var formattedMoney = MoneyFormatter.ToKMB(amount);
                for (int i = 0; i < goldTextArray.Length; i++)
                {
                    goldTextArray[i].text = formattedMoney;
                }
            };
        }

        // Button action
        public void OpenRanchScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}

