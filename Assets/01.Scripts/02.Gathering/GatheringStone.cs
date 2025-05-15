using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringStone : GatheringBase
{
    protected override void OnClickedFunc()
    {
        base.OnClickedFunc();
        transform.DOShakeRotation(1f, 1f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        Debug.Log("돌에서 아이템 획득");
    }
}
