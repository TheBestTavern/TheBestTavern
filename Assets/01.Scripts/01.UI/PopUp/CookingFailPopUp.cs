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

        base.OnClose();
    }

    public override void OnOpen()
    {
        canvasGroup.DOFade(1f, 1f);

        //failText.gameObject.SetActive(true);

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

    //현재 : 아이템네임 꺼짐, 아이템이미지꺼짐
    // 성공,페일 텍스트 둘다 켜짐
    public async void ShowInfo()
    {
        
        successText.gameObject.SetActive(false);
        failText.gameObject.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);

        
        bool isPlate = CookingMiniGameManager.Instance.TryCooking();
        if (isPlate)
        {
            var result = CookingMiniGameManager.Instance.GetMiniGameResult();
            int itemKey = RecipeManager.Instance.GetItemKey();

            Debug.Log($"최종 아이템 키 : {itemKey}");

            // 실패 조건 먼저 체크
            if (itemKey == -1 || result == CookingResultGrade.Failed)
            {
                successText.gameObject.SetActive(false);
                failText.gameObject.SetActive(true);
                itemImage.gameObject.SetActive(false);
                itemNameText.text = "요리에 실패했어요...";
                itemNameText.gameObject.SetActive(true);
                SoundManager.Instance.PlaySFX("Fail");
                return;
            }

            // 성공
            successText.gameObject.SetActive(true);
            failText.gameObject.SetActive(false);
            SoundManager.Instance.PlaySFX("Success");

            var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
            itemNameText.text = data.name;
            itemNameText.gameObject.SetActive(true);

            itemImage.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{data.englishName}.png", true);
            itemImage.gameObject.SetActive(itemImage.sprite != null);
        }
        else
        {
            // 조리 도구 실패
            failText.text = "조리 도구를 잘못 선택한 것 같다...";
            failText.gameObject.SetActive(true);
            itemNameText.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(false);
            successText.gameObject.SetActive(false);
            SoundManager.Instance.PlaySFX("Fail");
        }
    }
    }
