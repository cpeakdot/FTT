using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FTT.Consumable;

public class RecipeItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private ConsumableSO consumableSO;

    public void InitRecipeItem(Sprite icon, string recipeText, ConsumableSO consumableSO)
    {
        this.icon.sprite = icon;
        this.recipeText.SetText(recipeText);
        this.consumableSO = consumableSO;
    }

}
