using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingFailPopUp : BasePopUp
{
    [SerializeField] private TextMeshProUGUI successText; // 성공 텍스트 
    [SerializeField] private TextMeshProUGUI itemNameText; // 아이템 이름 텍스트
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트
    [SerializeField] private Image itemImage;


    public override void OnClose()
    {
        // 미니게임 닫기 
        //CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();
        failText.gameObject.SetActive(false);
        base.OnClose();
    }

    public override void OnOpen()
    {
        canvasGroup.DOFade(1f, 1f);

        failText.gameObject.SetActive(true);
       
        ShowInfo();

        try
        {
            base.OnOpen();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public async void ShowInfo()
    {
        if (!CookingMiniGameManager.Instance.TryCooking())
        {
            var result = CookingMiniGameManager.Instance.GetMiniGameResult();
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
            int itemKey = RecipeManager.Instance.GetItemKey();
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
            if (itemImage.sprite == null)
            {
                itemImage.gameObject.SetActive(false);
            }
            return;
        }
        else
        {
            failText.text = "조리 도구를 잘못 선택한 것 같다...";
            failText.gameObject.SetActive(true);
            SoundManager.Instance.PlaySFX("Fail");
        }
    }
}
