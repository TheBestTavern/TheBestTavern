using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static DesignEnums;
using static UnityEditor.Progress;

public class GatheringBase : MonoBehaviour
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

    protected async virtual void OnClickedFunc()
    {
        Debug.Log($"{gameObject.name} 클릭");

        int randInt = UnityEngine.Random.Range(0, 10);

        if (randInt > 2)
        {
            int itemId = GatheringManager.Instance.GetRandomItemID();
            Debug.Log(itemId);

            Data_Foods item = Data.GetRawItem(itemId);

            Sprite sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{item.englishName}.png", true);
            var pooledImage = await PoolManager.Instance.GetAddressable<ItemImage>(
            "ItemImage.prefab",
            Vector3.zero,
            GatheringManager.Instance.gatheringInventoryUI.transform);

            pooledImage.sprite = sprite;

            await ItemMoveAnim(pooledImage);

            if (InventoryManager.Instance.Invens[InvenType.Gathering].AcquireItem(item, 1))
            {
                Debug.Log("아이템 증가 가능");
            }
            else
            {
                Debug.Log("아이템 증가 불가능");
                await pooledImage.rectTransform
           .DOAnchorPosY(-1000, 0.7f)
           .SetEase(Ease.InOutQuad)
           .AsyncWaitForCompletion();
            }

            pooledImage.TriggerReturn();
        }
    }

    private async Task ItemMoveAnim(ItemImage pooledImage)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GatheringManager.Instance.canvsRect, Input.mousePosition, GatheringManager.Instance.uiCamera, out Vector2 localPoint);

        pooledImage.transform.SetAsFirstSibling();

        pooledImage.rectTransform.anchoredPosition = localPoint;
        pooledImage.rectTransform.localPosition = new Vector3(
            pooledImage.rectTransform.localPosition.x,
            pooledImage.rectTransform.localPosition.y,
            0f
        );

        pooledImage.rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        await pooledImage.rectTransform.DOAnchorPosY(localPoint.y + 200, 0.3f)
        .SetEase(Ease.OutSine)
        .OnComplete(() =>
        {
            pooledImage.rectTransform.DOAnchorPosY(localPoint.y, 0.5f)
                .SetEase(Ease.InBounce);
        }).AsyncWaitForCompletion();


        Vector2 targetPos = GatheringManager.Instance.invenRect.anchoredPosition;

        await pooledImage.rectTransform
            .DOAnchorPos(targetPos, 0.7f)
            .SetEase(Ease.InOutQuad)
            .AsyncWaitForCompletion();
    }
}
