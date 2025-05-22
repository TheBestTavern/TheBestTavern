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

    [SerializeField] private Image itemImage;

    [SerializeField] private CanvasGroup resultCanvasGroup;

    public override void OnClose()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();

        base.OnClose();
    }

    public override void OnOpen()
    {
        //setContents();

        resultCanvasGroup.DOFade(1f, 1f);

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
        int itemKey = RecipeManager.Instance.GetItemKey();
        var result = CookingMiniGameManager.Instance.GetMiniGameResult();

        Debug.Log($"최종 아이템 키 : {itemKey}");
        if (itemKey == -1 || result == CookingResultGrade.Failed)
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
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = data.name;

        itemImage.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{data.englishName}.png", true);
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }

    //public void ResetContents()
    //{
    //    itemNameText.text = null;
    //    itemImage.sprite = null;
    //}
}
