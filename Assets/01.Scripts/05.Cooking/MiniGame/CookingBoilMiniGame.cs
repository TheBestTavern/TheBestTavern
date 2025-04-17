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
    }

    public override void StopGame()
    {
    }

    protected override void UpdateGamePlay()
    {
    }
}
