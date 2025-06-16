using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private Image itemImage;

    [SerializeField] private CanvasGroup resultCanvasGroup;

    public override void OnClose()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();
        resultCanvasGroup?.DOKill();
        PopUpManager.Instance.PopUps.Remove(PopUpType.CookingResult);
        base.OnClose();
    }

    public override void OnOpen()
    {
        //setContents();
        Debug.Log("팝업열림");

        if (resultCanvasGroup != null)
        {
            resultCanvasGroup.DOKill();

            resultCanvasGroup.DOFade(1f, 1f);
        }
        itemNameText.gameObject.SetActive(true);
        itemImage.gameObject.SetActive(true);

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
        Debug.Log(result);
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
                itemImage.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public async void ShowItemInfo()
    {
        List<int> itemKeys = RecipeManager.Instance.GetItemKeys();
        var result = CookingMiniGameManager.Instance.GetMiniGameResult();

        if (itemKeys.Contains(-1) || result == CookingResultGrade.Failed)
        {
            successText.gameObject.SetActive(false);
            failText.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);
            itemNameText.text = ""; // 여기에 미니게임 실패시 문구 추가
            itemNameText.gameObject.SetActive(true);

            SoundManager.Instance.PlaySFX("Fail");
            return;
        }
        SoundManager.Instance.PlaySFX("Success");

        List<string> nameList = new();
        foreach (var key in itemKeys)
        {
            var data = DataManager.Instance.DataLoader_Foods.GetByKey(key);
            if (data != null)
            {
                nameList.Add(data.name);
            }
        }
        itemNameText.text = string.Join(", ", nameList);

        var firstData = DataManager.Instance.DataLoader_Foods.GetByKey(itemKeys.FirstOrDefault());
        itemImage.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", firstData.englishName, true);
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }

    //public void ResetContents()
    //{
    //    itemNameText.text = null;
    //    itemImage.sprite = null;
    //}
}
