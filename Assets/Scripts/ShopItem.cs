using FTT.Consumable;
using FTT.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private ShopManager shopManager;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    
    [SerializeField] private ConsumableSO consumableSO;
    [SerializeField] private int amount = 1;

    [SerializeField] private Text titleText;
    [SerializeField] private Text costText;

    private void Start()
    {
        shopManager = ShopManager.Instance;
        button.onClick.AddListener(() => { OnClickButton(); });
        image.sprite = consumableSO.icon;
    }

    private void OnEnable()
    {
        titleText.text = amount + " " + consumableSO.id;
        costText.text = consumableSO.marketPrice + " GOLD";
    }

    private void OnClickButton()
    {
        shopManager.BuyItem(consumableSO , amount);
    }
    
}
