using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingCuttingMiniGame_Test : CookingMiniGameBase
{
    [ SerializeField ] CookingKnife_Test knife;

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }
    public override void StartGame()
    {
 
    }

    public override void StopGame()
    {
        var grade = JudgeGrade();
        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
        RecipeManager.Instance.EndCooking();
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
