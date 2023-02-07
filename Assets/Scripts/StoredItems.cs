using FTT.Consumable;
using System.Collections.Generic;

public static class StoredItems
{
    /// <summary>
    /// Item list that player can craft.
    /// </summary>
    private static List<ConsumableSO> achievedConsumableIndexes = new List<ConsumableSO>();
    public static bool TryAddItemToAchievements(ConsumableSO consumable)
    {
        if (IsItemAchievable(consumable))
        {
            AddItemToTheList(consumable);
            return true;
        }
        return false;
    }

    public static bool IsItemAchievable(ConsumableSO consumable)
    {
        return !achievedConsumableIndexes.Contains(consumable);
    }

    private static void AddItemToTheList(ConsumableSO consumable)
    {
        achievedConsumableIndexes.Add(consumable);
    }
}
