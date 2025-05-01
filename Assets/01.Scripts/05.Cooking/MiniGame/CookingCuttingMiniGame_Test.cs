using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingCuttingMiniGame_Test : CookingMiniGameBase
{
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
    }

    protected override float GetTimer()
    {
        return data.GrillTimer;
    }

    protected override void UpdateGamePlay()
    {
        InstantGameOver();
    }

    public void InstantGameOver()
    {
        isGameOver = true;
    }

    public CookingResultGrade JudgeGrade()
    {
        // 임시로 무조건 최상위 등급 반환
        return CookingResultGrade.Legendary;
    }
}
