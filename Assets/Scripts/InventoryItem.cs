using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

namespace FTT.Controllers
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image iconSprite;
        [SerializeField] private Button button;

        public void SetItem(string name, Sprite icon)
        {
            nameText.SetText(name);
            iconSprite.sprite = icon;
        }
    }
}