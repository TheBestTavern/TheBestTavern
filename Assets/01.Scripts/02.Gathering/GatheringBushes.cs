using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringBushes : GatheringBase
{
    protected override void OnClickedFunc()
    {
        transform.DOShakeRotation(1f,10f);
        OffMouseFunc();
        PlaySFXSound();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        int randInt = Random.Range(0, 10);
        if (randInt == 0)
        {
            ForestGatheringManager.Instance.OnMiniGame("Forest_Animal_Dev");
            UIManager.Instance.gatheringSceneUI.SetMiniGameBackGround(true);
        }
        else
        {
            base.OnClickedFunc();

        }
    }    
}
