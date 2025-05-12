using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 요리 씬 인벤토리 UI
/// </summary>
public class CookingInventoryView : InventoryViewLoose
{
    [SerializeField] Button startMiniGameBtn;
    [SerializeField] Image btnImage;
    [SerializeField] Material grayscaleMaterial;
    [SerializeField] private ItemImage itemImage;

    protected override void OnEnable()
    {
        base.OnEnable();
        startMiniGameBtn.onClick.AddListener(CookingMiniGameManager.Instance.miniGameAnim.Invoke);
        //startMiniGameBtn.onClick.AddListener(CookingMiniGameManager.Instance.ClickStartButton);
        startMiniGameBtn.onClick.AddListener(() => gameObject.SetActive(false));
        DisableButton();
    }

    protected virtual void OnDisable()
    {
        //base.OnDisable();

        startMiniGameBtn.onClick.RemoveAllListeners();
    }

    public void OnSelectTool(string s)
    {
        SetTargetSlotCount(s);
    }

    public void OnDeselectTool()
    {
        gameObject.SetActive(false);
        SetTargetSlotCount("none");
        foreach (var slotindex in targetingSlots.ToList())
        {
            아이템타게팅취소(slotindex);
            index2Slots[slotindex].ExitTargetingState();
        }
    }

    public void SetAbleButton()
    {
        if (targetingSlots.Count >= minTargetingNum && targetingSlots.Count <= maxTargetingNum)
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

    public void SetTargetSlotCount(string s)
    {
        switch (s)
        {
            case "none":
                minTargetingNum = 0;
                maxTargetingNum = 0;
                break;
            case "Cooking_Grill_Test":
                minTargetingNum = 1;
                maxTargetingNum = 1;
                break;
            case "Cooking_Grind_Test":
                minTargetingNum = 1;
                maxTargetingNum = 1;
                break;
            case "Cooking_Mill_Test":
                minTargetingNum = 1;
                maxTargetingNum = 1;
                break;
            case "Cooking_Cutting_Test":
                minTargetingNum = 1;
                maxTargetingNum = 1;
                break;
            case "Cooking_Boil_Test":
                minTargetingNum = 1;
                maxTargetingNum = 1;
                break;
            case "Plate":
                minTargetingNum = 2;
                maxTargetingNum = 2;
                break;
            default:
                break;
        }
        SetAbleButton();
    }

    public async override void 아이템타게팅(int index)
    {
        base.아이템타게팅(index);

        //ItemStack item = index2Slots[index].GetSlotItem();

        //CookingMiniGameManager.Instance.SetMiniGameItem(item);

        //var pooledImage = PoolManager.Instance.Get<ItemImage>(itemImage, index2Slots[index].GetComponent<RectTransform>().position, transform);
        var pooledImage = await PoolManager.Instance.GetAddressable<ItemImage>("ItemImage.prefab", index2Slots[index].GetComponent<RectTransform>().position, transform);
        pooledImage.gameObject.SetActive(true);
        pooledImage.sprite = index2Slots[index].image.sprite;

        RectTransform rect = pooledImage.GetComponent<RectTransform>();
        rect.position = index2Slots[index].GetComponent<RectTransform>().position;
        rect.DOScale(new Vector3(2f, 2f, 2f), 1.5f);
        rect.DOAnchorPos(new Vector2(0, 0), 1.5f).OnComplete(() =>
        {
            pooledImage.TriggerReturn();
            rect.localScale = new Vector3(1, 1, 1);
        });
    }
}
