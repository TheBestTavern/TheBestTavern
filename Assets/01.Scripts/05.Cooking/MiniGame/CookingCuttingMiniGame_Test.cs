using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingCuttingMiniGame_Test : CookingMiniGameBase
{
    [ SerializeField ] CookingKnife_Test knife;
    [SerializeField] CookingIngredientSO ingredientSO;

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }
    public override async void StartGame()
    {
        int itemKey = CookingMiniGameManager.Instance.GetSelectedItemFoodCategory();
        ingredientSO = CookingMiniGameManager.Instance.GetSelectdItemSO(itemKey);
        if (ingredientSO != null)
        {
            string prefabAdress = ingredientSO.prefabAdress;
            GameObject prefab = await AddressablesLoader.Instance.AddressablesLoadAsync(prefabAdress);
            if (prefab != null)
            {
                Instantiate(prefab);
            }
            else
            {
                GameObject defaultPrefab = await AddressablesLoader.Instance.AddressablesLoadAsync("GreenLong");
                Instantiate(defaultPrefab);

                //string defalutAdress = "";
                //GameObject defaultPrefab = await AddressablesLoader.Instance.AddressablesLoadAsync(defalutAdress);
            }
        }
        else
        {
            GameObject defaultPrefab = await AddressablesLoader.Instance.AddressablesLoadAsync("GreenLong");
            Instantiate(defaultPrefab);
        }
    }

    public override void StopGame()
    {
        RecipeManager.Instance.EndCooking();
        var grade = JudgeGrade();
        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
    }

    protected override float GetTimer()
    {
        return data.CutTimer;
    }

    protected override void UpdateGamePlay()
    {
       
    }

    public CookingResultGrade JudgeGrade()
    {
        float ratio = knife.GetPiecesRatio();
        Debug.Log($"자른 비율:{ratio}");

        // 조각 크기 평균
        if (ratio >= data.PerfectRatio)
        {
            return CookingResultGrade.Legendary;
        }
        else if (ratio >= data.GoodRatio)
        {
            return CookingResultGrade.Rare;
        }
        else if (ratio >= data.BadRatio)
        {
            return CookingResultGrade.Common;
        }
        else
        {
            return CookingResultGrade.Failed;
        }
    }
}
