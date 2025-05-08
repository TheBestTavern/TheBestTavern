using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringBushes : GatheringProps
{
    protected override void OnClickedFunc()
    {
        transform.DOShakeRotation(1f,10f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        int randInt = Random.Range(0, 10);
        if (randInt == 0)
        {
            ForestGatheringManager.Instance.OnMiniGame("Forest_Animal_Dev");
        }
        else
        {
            base.OnClickedFunc();
        }
    }    
}
