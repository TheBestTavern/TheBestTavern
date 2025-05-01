using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 휘젓기 미니게임
/// (결과 : 조합재료)
/// </summary>
public class CookingMixingBowlMiniGame : CookingMiniGameBase
{
    public override void StartGame()
    {
    }

    public override void StopGame()
    {
    }

    protected override float GetTimer()
    {
        return data.MixingBowlTimer;
    }

    protected override void UpdateGamePlay()
    {
    }
}
