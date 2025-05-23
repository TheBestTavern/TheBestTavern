using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringHole : GatheringBase
{
    protected override void OnClickedFunc()
    {
        base.OnClickedFunc();
        transform.DOShakeRotation(1f, 1f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        Debug.Log("구멍에서 아이템 획득");
        SoundManager.Instance.PlaySFX("GrindButton");
    }
}
