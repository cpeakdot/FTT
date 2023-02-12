using UnityEngine;

namespace FTT.Managers
{
    public partial class InventoryManager : MonoBehaviour
    {
        private void SaveItem(Consumable.Consumable consumable , int amount = 1)
        {
            PlayerPrefs.SetInt(consumable.GetScriptableObject.id , amount);
        }

        private void ReAdjustItemCount(Consumable.Consumable consumable , int amount = 1)
        {
            PlayerPrefs.SetInt(consumable.GetScriptableObject.id , amount);
        }

        private void DeleteKey(Consumable.Consumable consumable)
        {
            PlayerPrefs.DeleteKey(consumable.GetScriptableObject.id);
        }

        private int GetKey(Consumable.Consumable consumable)
        {
            return PlayerPrefs.GetInt(consumable.GetScriptableObject.id, 0);
        }
    }
}