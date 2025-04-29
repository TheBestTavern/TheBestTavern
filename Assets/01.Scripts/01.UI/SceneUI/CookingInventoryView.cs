using System;
using System.Collections;
using System.Collections.Generic;
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
}
