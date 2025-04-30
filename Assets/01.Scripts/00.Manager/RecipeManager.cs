using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 조합 확인
/// </summary>
public class RecipeManager : MonoSingleton<RecipeManager>
{
    
    // 도구
   // private Data_CookingSteps cookingSteps;

    private List<Data_Foods> currentIngredients; // 현재 사용중인 재료
    private int currentTool; // 현재 사용 중인 도구
    private Data_Recipes currentRecipe; // 현재 레시피

    public int resultItemKey = -1;

    Dictionary<string, int> toolTable = new()
        {
            //{ DesignEnums.CookingToolType.doma, 1001 },
            //{ DesignEnums.CookingToolType.julgu, 1002 },
            //{ DesignEnums.CookingToolType.matdol, 1003 },
            //{ DesignEnums.CookingToolType.gamasot, 1004 },
            //{ DesignEnums.CookingToolType.sotdduggung, 1005 },
            //{ DesignEnums.CookingToolType.mixingbowl, 1006 },
            //{ DesignEnums.CookingToolType.dish, 1007 }
            {"Cooking_Grill_Test", 1006 },
            {"Cooking_Grind_Test", 1002 },
            {"Cooking_Mill_Test", 1003 }
        };

    public void StartCooking(List<Data_Foods> ingredients, string tool)
    {
        currentIngredients = ingredients;

        toolTable.TryGetValue(tool, out int toolId);
        currentTool = toolId;
        //currentRecipe = GetRecipe(ingredients, toolId);
        // 수정
        

        if (currentRecipe == null)
        {
            // 무조건 '실패한 요리' 나와야 함
            Debug.Log($"재료 :{currentIngredients} 도구 : {currentTool} / 요리 실패 : 해당하는 레시피 없음");
        }
        else 
        {
            int category = currentRecipe.resultCategory;
            Debug.Log($"요리 시작 레시피 : {currentRecipe}"); 
        }
    }
    
    public int EndCooking()
    {
        
        var grade = CookingMiniGameManager.Instance.GetMiniGameResult();

        // 레시피대로 조리하지 않으면 무조건 실패
        if (currentRecipe == null) 
        {
            grade = CookingResultGrade.Failed;
            CookingMiniGameManager.Instance.SetMiniGameResult(grade);
            resultItemKey = -1;
            return -1; 
        }

        resultItemKey = GetItemKey(currentRecipe.resultCategory, grade);
        return resultItemKey;
    }

    /// <summary>
    /// 일치하는 레시피 찾기
    /// </summary>
    /// <param name="ingredients"></param>
    /// <param name="tool"></param>
    /// <returns></returns>
    public Data_Recipes GetRecipe(List<Data_Foods> ingredients, int toolId)
    {
        
        // 재료 리스트에서 음식군ID 가져오기
       var ingredientsSet = new HashSet<int>();
        foreach (var ingredient in ingredients) 
        {
           //ingredientsSet.Add(ingredient.카테고리);
        }

        // 재료와 tool의 조합이 레시피대로인지 확인
        foreach (var recipe in DataManager.Instance.Dataloader_Recipes.ItemsList) 
        {
            if (recipe.usingTool != toolId) continue;

            
            var recipeSet = new HashSet<int>(recipe.ingredients);

            
            if (recipeSet.SetEquals(ingredientsSet)) return recipe;
        }     
        return null; 
    }

    /// <summary>
    /// 레시피의 결과음식군과 미니게임결과등급 조합해서 최종 아이템키 반환
    /// </summary>
    public int GetItemKey(int resultCategory, CookingResultGrade grade)
    {
       
        var dict = DataManager.Instance.DataLoader_FoodCategory.ItemsDict;
        dict.TryGetValue(resultCategory, out var data);

            return grade switch {
                CookingResultGrade.Legendary => data.goodFoodID,
                CookingResultGrade.Rare => data.sosoFoodID,
                CookingResultGrade.Common => data.badFoodID,
                CookingResultGrade.Failed => -1,
                _ => -1
            };
    }
}
