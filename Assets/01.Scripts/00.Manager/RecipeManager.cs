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
    private List<Data_Foods> currentIngredients; // 현재 사용중인 재료
    private int currentTool; // 현재 사용 중인 도구
    private Data_Recipes currentRecipe; // 현재 레시피

    public int resultItemKey = -1;

    public event Action<int> OnCookingEnded;

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
            {"Cooking_Mill_Test", 1003 },
            {"Cooking_Cutting_Test", 1001 },
            {"Plate", 1008 },
            {"Cooking_MixingBowl_Test", 1007 }
            //{"Cooking_Boil_Test", 1004, 1005 }
        };

    public void StartCooking(List<Data_Foods> ingredients, string tool)
    {
        currentIngredients = ingredients;

        toolTable.TryGetValue(tool, out int toolId);
        currentTool = toolId;
        currentRecipe = GetRecipe(ingredients, toolId);
        

        if (currentRecipe == null)
        {
            // 무조건 '실패한 요리' 나와야 함
            Debug.Log($"도구 : {currentTool}, 요리 실패 : 해당하는 레시피 없음");
        }
        else 
        {
            int category = currentRecipe.resultCategory;
            Debug.Log($"요리 시작 레시피 : {currentRecipe.name}"); 
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
        OnCookingEnded?.Invoke(resultItemKey);
        return resultItemKey;
    }

    // !!임시 코드입니다. 미니게임 계산식에 따라 수정해야 함
    // 접시에서 사용
    public int CompleteDish()
    {
        //현재 무조건 전설등급이 나오게 되어있으나 조합 규칙대로 수정해야함
        //var grade = CookingResultGrade.Legendary;
        //CookingMiniGameManager.Instance.SetMiniGameResult(grade);
        if (currentRecipe == null)
        {
            resultItemKey = -1;
            return -1;
        }

        var combineGrade = JudgeCombineGrade(currentIngredients);
        CookingMiniGameManager.Instance.SetMiniGameResult(combineGrade);
        resultItemKey = GetItemKey(currentRecipe.resultCategory, combineGrade);
        OnCookingEnded?.Invoke(resultItemKey);
        return resultItemKey;
    }

    // 재료들 조합 결과 
    // (믹싱볼에서 사용) 
    public int CombineIngredients()
    {
        // 흐름
        // 미니게임 끝난다 -> fail 됐다 -> fail 들어오면 무조건 실패처리 -1 반환
        // 미니게임 끝났다 -> fail 한개도 없다 -> 레시피 확인한다
        // -> 레시피 틀렸따 -1반환
        // -> 레시피맞았다 -> 조합식에 따라서 최종키반환

        var grade = CookingMiniGameManager.Instance.GetMiniGameResult();
        if(grade == CookingResultGrade.Failed)
        {
            resultItemKey = -1;
            return -1;
        }
        if (currentRecipe == null)
        {
            resultItemKey = -1;
            return -1;
        }

        var combineGrade = JudgeCombineGrade(currentIngredients);
        CookingMiniGameManager.Instance.SetMiniGameResult(combineGrade);
        int key = GetItemKey(currentRecipe.resultCategory, combineGrade);
        //OnCookingEnded?.Invoke(resultItemKey);
        return key;
    }

    /// <summary>
    /// 재료 2개이상 조합 시 결과등급 반환 메서드
    /// </summary>
    /// <param name="ingredients"></param>
    /// <returns></returns>
    private CookingResultGrade JudgeCombineGrade(List<Data_Foods> ingredients)
    {
        bool hasCommon = false;
        bool hasRare = false;
        bool hasLegendary = false;

        foreach (var ingredient in ingredients)
        {
            switch (ingredient.grade)
            {
                case DesignEnums.GradeType.common:
                    hasCommon = true; break;
                case DesignEnums.GradeType.rare:
                    hasRare = true; break;
                case DesignEnums.GradeType.legendary:
                    hasLegendary = true;  break;
            }
        }
        // 모두 레전더리 : Legendary
        // 레전더리 없이 Rare이상만 있음 : Rare
        // Common이 하나라도 포함되면 : common
        if (hasCommon) return CookingResultGrade.Common;
        if (hasRare && !hasLegendary) return CookingResultGrade.Rare;
        if (hasLegendary && !hasCommon) return CookingResultGrade.Common;

        return CookingResultGrade.Common; //기본값
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
           ingredientsSet.Add(ingredient.FoodCategory);
            Debug.Log($"재료 음식군 번호 : {ingredient.FoodCategory}");
        }

        // 재료와 tool의 조합이 레시피대로인지 확인
        foreach (var recipe in DataManager.Instance.Dataloader_Recipes.ItemsList) 
        {
            if (recipe.usingTool != toolId) continue;

            
            var recipeSet = new HashSet<int>(recipe.ingredients);

           
            if (recipeSet.SetEquals(ingredientsSet)) 
                return recipe;
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

    public int GetItemKey()
    {
        return resultItemKey;
    }
}
