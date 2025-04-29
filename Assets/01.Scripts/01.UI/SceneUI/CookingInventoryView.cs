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
    [SerializeField] Button startMiniGameBtn;
    [SerializeField] Image btnImage;
    [SerializeField] Material grayscaleMaterial;
    [SerializeField] private Image ItemImage;

    protected override void OnEnable()
    {
        base.OnEnable();
        startMiniGameBtn.onClick.AddListener(CookingMiniGameManager.Instance.ShowMiniGame);
        startMiniGameBtn.onClick.AddListener(() => gameObject.SetActive(false));
        DisableButton();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        startMiniGameBtn.onClick.RemoveAllListeners();
    }

    public void SetAbleButton()
    {
        if(targetingSlots.Count >= minTargetingNum && targetingSlots.Count <= maxTargetingNum)
        {
            EnableButton();
        }
        else
        {
            DisableButton();
        }
    }

    private void EnableButton()
    {
        startMiniGameBtn.enabled = true;
        btnImage.material = default;
    }

    private void DisableButton()
    {
        startMiniGameBtn.enabled = false;
        btnImage.material = grayscaleMaterial;
    }

    public void SetTargetSlotCount(int min, int max)
    {
        minTargetingNum = min;
        maxTargetingNum = max;
    }
  
    public override void 아이템타게팅(int index)
    {
        base.아이템타게팅(index);

        //ItemStack item = index2Slots[index].GetSlotItem();

        //CookingMiniGameManager.Instance.SetMiniGameItem(item);

        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = index2Slots[index].image.sprite;

        RectTransform rect = ItemImage.GetComponent<RectTransform>();
        rect.position = index2Slots[index].GetComponent<RectTransform>().position;
        rect.DOScale(new Vector3(2f, 2f, 2f), 1.5f);
        rect.DOAnchorPos(new Vector2(0, 0), 1.5f).OnComplete(() =>
        {
            ItemImage.gameObject.SetActive(false);
            rect.localScale = new Vector3(1, 1, 1);
        });
    }
}
