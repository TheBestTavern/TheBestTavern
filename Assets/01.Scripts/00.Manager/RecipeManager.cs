using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 조합 확인
/// </summary>
public class RecipeManager : MonoSingleton<RecipeManager>
{
    // 레시피 
    private Data_RecipesLoader loader;

    // 도구
    private Data_CookingSteps cookingSteps;

    private List<int> currentIngredients; // 현재 사용중인 재료
    private DesignEnums.CookingToolType currentTool; // 현재 사용 중인 도구
    private Data_Recipes currentRecipe; // 현재 레시피

    public void StartCooking(List<int> ingredients, DesignEnums.CookingToolType tool)
    {
        currentIngredients = ingredients;
        currentTool = tool;
        currentRecipe = GetResipe(ingredients, tool);

        if (currentRecipe == null)
        {
            // 무조건 '실패한 요리' 나와야 함
        }
    }

    public Data_Recipes GetResipe(List<int> ingredients, DesignEnums.CookingToolType tool)
    {
        // 재료와 tool의 조합이 레시피대로인지 확인

        //doma = 0,
        //julgu = 1,
        //matdol = 2,
        //gamasot = 3,
        //sotdduggung = 4,
        //mixingbowl = 5,
        //dish = 6,

            return null;
    }

}
