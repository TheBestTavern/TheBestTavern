using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static DesignEnums;

public class GatheringProps : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected bool isClicked = false;

    Vector3 scale;

    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        scale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        if (!isClicked)
        {
            OnMouseFunc();
        }
    }

    private void OnMouseExit()
    {
        if (!isClicked)
        {
            OffMouseFunc();
        }
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            OnClickedFunc();
            isClicked = true;
        }
    }

    protected virtual void OnMouseFunc()
    {
        transform.DOScale(transform.localScale * 1.05f, 0.5f);
    }

    protected virtual void OffMouseFunc()
    {
        transform.DOScale(scale, 0.5f);
    }

    protected virtual void OnClickedFunc()
    {
        Debug.Log($"{gameObject.name} 클릭");
        int itemId = ForestGatheringManager.Instance.GetRandomItemID();
        Debug.Log(itemId);
    }
}
