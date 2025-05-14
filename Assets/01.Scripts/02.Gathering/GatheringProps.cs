using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
        if (EventSystem.current.IsPointerOverGameObject()) return;
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
        if (EventSystem.current.IsPointerOverGameObject()) return;
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

        int randInt = Random.Range(0,10);

        if (randInt > 2)
        {
            int itemId = GatheringManager.Instance.GetRandomItemID();
            Debug.Log(itemId);

            if (InventoryManager.Instance.Invens[InvenType.Gathering].아이템획득(Data.GetRawItem(itemId), 1))
            {
                Debug.Log("아이템 증가 가능");
            }
            else
            {
                Debug.Log("아이템 증가 불가능");
            }
        }
    }
}
