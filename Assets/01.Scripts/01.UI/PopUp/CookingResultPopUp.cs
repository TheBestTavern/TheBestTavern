using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingResultPopUp : BasePopUp
{
    //private TextMeshProUGUI promptText; // 아이템 설명
    [SerializeField] private TextMeshProUGUI successText; // 성공 텍스트 
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트
    [SerializeField] private TextMeshProUGUI itemNameText; // 아이템 이름 텍스트


    [SerializeField] private CanvasGroup resultCanvasGroup;

    public override void OnClose()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();
        base.OnClose();
    }

    public override void OnOpen()
    {
        resultCanvasGroup.DOFade(1f, 1f);
        ShowResultText();
        ShowItemInfo();
        try
        {
            base.OnOpen();
        }
        catch (System.Exception e) 
        {
            Debug.LogError(e);
        }
    }

    public void ShowResultText()
    {
        var result = CookingMiniGameManager.Instance.GetMiniGameResult();
        
        // 결과에 따라 성공/실패 텍스트 활성
         switch (result)
        {
            case CookingResultGrade.Legendary:
            case CookingResultGrade.Rare:
            case CookingResultGrade.Common:
                successText.gameObject.SetActive(true);
                failText.gameObject.SetActive(false);
                break;

            case CookingResultGrade.Failed:
                successText.gameObject.SetActive(false);
                failText.gameObject.SetActive(true);
                break;
        }
    }

    public void ShowItemInfo()
    {

        int itemKey = RecipeManager.Instance.EndCooking();
        if (itemKey == -1)
        {
            Debug.Log("요리 실패해서 이름 안 뜸");
            itemNameText.gameObject.SetActive(false);
            return;
        }
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = data.name;
    }
}
