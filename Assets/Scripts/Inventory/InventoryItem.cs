using FTT.Farm;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

namespace FTT.Controllers
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image iconSprite;
        [SerializeField] private Button button;
        private int count = 1;

        public void SetItem(string name, int count, Sprite icon, Consumable.Consumable plant)
        {
            nameText.SetText(name);
            this.count = count;
            countText.SetText(count.ToString());
            iconSprite.sprite = icon;
            button.onClick.AddListener(() =>
            {
                FarmingManager.Instance.SetPlant(plant);
            });
        }
    }
}