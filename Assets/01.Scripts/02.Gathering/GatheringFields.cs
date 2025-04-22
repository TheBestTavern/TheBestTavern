using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GatheringFields : GatheringProps
{
    [SerializeField] private Tilemap onClickTilemap;

    protected override void OnMouseFunc()
    {
        base.OnMouseFunc();
        DOTween.ToAlpha(() => onClickTilemap.color, color => onClickTilemap.color = color, 0.2f, 0.5f);
    }

    protected override void OffMouseFunc()
    {
        base.OffMouseFunc();
        DOTween.ToAlpha(() => onClickTilemap.color, x => onClickTilemap.color = x, 0.0f, 0.5f);
    }

    protected override void OnClickedFunc()
    {
        base.OnClickedFunc();
        onClickTilemap.color = new Color(0, 0, 0, 0.2f);
        Debug.Log("밭에서 아이템 획득");
    }
}
