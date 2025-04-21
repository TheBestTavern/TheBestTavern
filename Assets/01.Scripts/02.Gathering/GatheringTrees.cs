using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringTrees : GatheringProps
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    protected override void OnMouseFunc()
    {
        base.OnMouseFunc();
        gameObject.transform.DOScale(1.05f, 0.5f);
    }

    protected override void OffMouseFunc()
    {
        base.OffMouseFunc();
        gameObject.transform.DOScale(1, 0.5f);
    }

    protected override void OnClickedFunc()
    {
        base.OnClickedFunc();
        transform.DOShakeRotation(1f, 1f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        Debug.Log("나무에서 아이템 획득");
    }
}
