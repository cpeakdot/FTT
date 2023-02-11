using UnityEngine;
using FTT.Consumable;

namespace FTT
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildingUIObject;
        [SerializeField] private GameObject[] otherCanvases;
        [SerializeField] private Transform recipeItemParent;
        [SerializeField] private GameObject recipeItemPrefab;

        public enum BuildingType
        {
            Oven,
            Popcorn,
        }

        [ContextMenu("Test")]
        public void Test()
        {
            ActivateBuildingCanvas(BuildingType.Oven);
        }

        [SerializeField] private BuildingTypeRecipes[] buildingTypeRecipesArray;
  
        public void ActivateBuildingCanvas(BuildingType buildingType)
        {
            for (int i = 0; i < otherCanvases.Length; i++)
            {
                otherCanvases[i].SetActive(false);
            }

            // Delete objects in the recipeItemParent
            for (int i = 0; i < recipeItemParent.childCount; i++)
            {
                DestroyImmediate(recipeItemParent.GetChild(i).gameObject);
            }

            buildingUIObject.SetActive(true);
            CreateRecipes(buildingType);
        }

        private ConsumableSO[] GetRecipes(BuildingType buildingType)
        {
            for (int i = 0; i < buildingTypeRecipesArray.Length; i++)
            {
                if(buildingTypeRecipesArray[i].buildingType == buildingType)
                {
                    return buildingTypeRecipesArray[i].recipes;
                }
            }
            return null;
        }

        private void CreateRecipes(BuildingType buildingType)
        {
            var recipes = GetRecipes(buildingType);
            for (int i = 0; i < recipes.Length; i++)
            {
                var newRecipe = Instantiate(recipeItemPrefab, Vector3.zero, Quaternion.identity, recipeItemParent);
                if(newRecipe.TryGetComponent(out RecipeItemUI recipeItem))
                {
                    string recipeText = "";
                    for (int j = 0; j < recipes[i].ingredients.Length; j++)
                    {
                        recipeText += recipes[i].ingredients[j].count + "x " + recipes[i].ingredients[j].consumable.id + "\n";
                    }
                    recipeItem.InitRecipeItem(recipes[i].icon, recipeText, recipes[i]);
                }
            }
        }
    }


    [System.Serializable]
    public class BuildingTypeRecipes
    {
        public BuildingUI.BuildingType buildingType;
        public ConsumableSO[] recipes;
    }
}


