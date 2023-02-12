using UnityEngine;
using FTT.Farm;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    private FarmingManager farmingManager;

    [SerializeField] private FarmingManager.SelectedObj selection;
    [SerializeField] private Image icon;

    private void Start()
    {
        farmingManager = FarmingManager.Instance;
        farmingManager.OnActionChange += OnActionChange;
    }

    private void OnActionChange(FarmingManager.SelectedObj selectedObj)
    {
        var iconColor = icon.color;

        if(selectedObj == selection)
        {
            iconColor.a = 1f;
        }
        else
        {
            iconColor.a = .5f;
        }

        icon.color = iconColor;
    }
}
