using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingDish : MonoBehaviour
{
    void Awake()
    {
        // 시작
        var items = CookingMiniGameManager.Instance.GetMiniGameItem();
        RecipeManager.Instance.StartCooking(items, "dish");

        // 마무리 버튼 클릭 시
        RecipeManager.Instance.CompleteDish();
        CookingMiniGameManager.Instance.GetCookingResultData();
    }
}
