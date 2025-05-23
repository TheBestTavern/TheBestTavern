using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GatheringFields : GatheringBase
{
    protected override void OnClickedFunc()
    {
        base.OnClickedFunc();
        transform.DOShakeRotation(1f, 1f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        Debug.Log("밭에서 아이템 획득");
        PlaySFXSound();
    }
}
