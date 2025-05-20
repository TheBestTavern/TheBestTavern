using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// 휘젓기 미니게임
/// (결과 : 조합재료)
/// </summary>
public class CookingMixingBowlMiniGame : CookingMiniGameBase
{
    private bool isSuccess = false;
    [SerializeField] private MixingSpoon mixingSpoon;
    [SerializeField] private CookingMixingBowlUI mixingbowlUI;
    private float mixingTime = 0;

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
        RecipeManager.Instance.CombineIngredients();

        //switch (grade) 
        //{
        //    // 성공한 경우 조합 로직 타야함
        //    case CookingResultGrade.Legendary:
        //    case CookingResultGrade.Rare:
        //    case CookingResultGrade.Common:
        //        RecipeManager.Instance.CombineIngredients();
        //        break;
        //    // 실패한 경우 원래 로직 그대로
        //    case CookingResultGrade.Failed:
        //        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
        //        break;
        //}
    }

    protected override float GetTimer()
    {
        return data.MixingBowlTimer;
    }
    protected override void UpdateGamePlay()
    {
        // 15초 내에 5초동안 마우스 입력이 있으면 성공
        // 없으면 Fail

        if (mixingSpoon.isDragging)
        {
            SoundManager.Instance.PlaySFX("MixingBowl");
            mixingTime += Time.deltaTime;
            mixingbowlUI.UpdateUI(mixingTime);
            if (mixingTime > data.MixingTime)
            {
               isSuccess = true;
               isGameOver = true;
            }
        }
    }
    
    CookingResultGrade JudgeGrade()
    {
        if (isSuccess)
        {
            return CookingResultGrade.Legendary;
        }
        else
        {
            return CookingResultGrade.Failed;
        }
    }
}
