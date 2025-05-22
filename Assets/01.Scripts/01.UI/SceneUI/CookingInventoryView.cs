using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

/// <summary>
/// 요리 씬 인벤토리 UI
/// </summary>
public class CookingInventoryView : InventoryViewLoose
{
    [SerializeField] Button startMiniGameBtn;
    [SerializeField] Image btnImage;
    [SerializeField] Material grayscaleMaterial;
    private Queue<ItemImage> pooledImageQueue = new();

    protected override void OnEnable()
    {
        base.OnEnable();
        startMiniGameBtn.onClick.AddListener(() =>
        {
            if (!CookingMiniGameManager.Instance.TryCooking())
            {
                UIManager.Instance.cookingSceneUI.OnClickBlurBackGround();
                if (pooledImageQueue.Count > 0)
                {
                    DisappearImage(pooledImageQueue.Dequeue());
                }
                return;
            }
            CookingMiniGameManager.Instance.miniGameAnim.Invoke();
            if (GameManager.Instance.isAnalyticsAgreed)
            {
                var CookingMiniGameData = new AnalyticsCookingMiniGame("CookingMiniGameData")
                {
                    miniGameName = CookingMiniGameManager.Instance.selectedCookingTool
                };
                AnalyticsService.Instance.RecordEvent(CookingMiniGameData);
            }
        });
        //startMiniGameBtn.onClick.AddListener(CookingMiniGameManager.Instance.ClickStartButton);
        startMiniGameBtn.onClick.AddListener(() => gameObject.SetActive(false));
        startMiniGameBtn.onClick.AddListener(() => UIManager.Instance.cookingSceneUI.ButtonsBack());
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
            case "Cooking_MixingBowl_Test":
                minTargetingNum = 2;
                maxTargetingNum = 3;
                break;
            case "Plate":
                minTargetingNum = 2;
                maxTargetingNum = 3;
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
        if(pooledImageQueue.Count == maxTargetingNum)
        {
            DisappearImage(pooledImageQueue.Dequeue());
        }
        var pooledImage = await PoolManager.Instance.GetAddressable<ItemImage>("ItemImage.prefab", index2Slots[index].GetComponent<RectTransform>().position, transform);
        pooledImageQueue.Enqueue(pooledImage);
        pooledImage.gameObject.SetActive(true);
        pooledImage.sprite = index2Slots[index].image.sprite;

        var pooledImageRect = pooledImage.GetComponent<RectTransform>();
        MoveToCenterImage(pooledImage);
    }

    public override void 아이템타게팅취소(int index)
    {
        base.아이템타게팅취소(index);
        if (pooledImageQueue.Count > 0)
        {
            DisappearImage(pooledImageQueue.Dequeue());
        }
    }

    void MoveToCenterImage(ItemImage itemImage)
    {
        RectTransform pooledImageRect = itemImage.GetComponent<RectTransform>();
        pooledImageRect.DOKill();
        pooledImageRect.DOScale(new Vector3(2f, 2f, 2f), 1.5f);
        pooledImageRect.DOAnchorPos(new Vector2(0, 0), 1.5f).OnComplete(() =>
        {
            pooledImageRect.DOAnchorPos(new Vector2(0, 50), 1.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        });
    }

    void DisappearImage(ItemImage itemImage)
    {
        RectTransform pooledImageRect = itemImage.GetComponent<RectTransform>();

        pooledImageRect.DOKill();

        pooledImageRect.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack);
        pooledImageRect.DOScale(0.1f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            itemImage.TriggerReturn();
        });
    }
}
