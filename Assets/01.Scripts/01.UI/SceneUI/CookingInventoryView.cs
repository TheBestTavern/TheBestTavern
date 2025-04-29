using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// 요리 씬 인벤토리 UI
/// </summary>
public class CookingInventoryView : InventoryViewLoose
{
    [SerializeField] private Image ItemImage;
    public override void 아이템타게팅(int index)
    {
        base.아이템타게팅(index);
        ItemStack item = index2Slots[index].GetSlotItem();

        CookingMiniGameManager.Instance.SetMiniGameItem(item);

        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = Resources.Load<Sprite>($"Item/{item.Origin.englishName}");
        RectTransform rect = ItemImage.GetComponent<RectTransform>();
        rect.position = index2Slots[index].GetComponent<RectTransform>().position;
        rect.DOScale(new Vector3(2f, 2f, 2f), 1.5f);
        rect.DOAnchorPos(new Vector2(0, 0), 1.5f).OnComplete(() => 
        { 
            ItemImage.gameObject.SetActive(false);
            rect.localScale = new Vector3(1,1,1);
        });
    }

}
