using FTT.Consumable;
using System.Collections.Generic;
using UnityEngine;

namespace cpeak.Tools
{
    public class ObjectProbability : MonoBehaviour
    {
        public List<ObjectWeight<ConsumableSO>> objectWeightList = new ();
        private static List<ObjectWeight<ConsumableSO>> objectWeights = new();
        static float totalWeight = 0;

        private void Start()
        {
            totalWeight = 0;
            objectWeights = objectWeightList;
        }

        public static ConsumableSO GetObjectToSpawn()
        {
            totalWeight = 0;

            var templist = new List<ObjectWeight<ConsumableSO>>();

            for (int i = 0; i < objectWeights.Count; i++)
            {
                templist.Add(objectWeights[i]);
            }

            for (int i = 0; i < templist.Count; i++)
            {
                if(!StoredItems.IsItemAchievable(templist[i].obj))
                {
                    templist.Remove(templist[i]);
                }
            }

            for (int i = 0; i < templist.Count; i++)
            {
                totalWeight += templist[i].weight;
            }

            float randomNumber = 0;

            do
            {
                if(totalWeight == 0)
                {
                    return null;
                }
                randomNumber = Random.Range(0 , totalWeight);
            } 
            while (randomNumber == totalWeight);

            for (int i = 0; i < templist.Count; i++)
            {
                if(randomNumber < templist[i].weight)
                {
                    return templist[i].obj;
                }
                randomNumber -= templist[i].weight;
            }
            return null;
        }
    }

    [System.Serializable]
    public class ObjectWeight<T>
    {
        public T obj;
        public int weight;
    }
}