using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingBoilMiniGame : CookingMiniGameBase
{
    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }

    public override void StartGame()
    {
        isGameOver = false;
        elapsedTimer = 0f;
        playTime = 0f;
        timer = 15f;
    }

    public override void StopGame()
    {
    }

    protected override void UpdateGamePlay()
    {
    }
}
