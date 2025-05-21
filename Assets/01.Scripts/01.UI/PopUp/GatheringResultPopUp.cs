using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GatheringResultPopUp : BasePopUp
{
    [SerializeField] private TextMeshProUGUI successText; // 성공 텍스트 
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트
    [SerializeField] private TextMeshProUGUI itemNameText; // 아이템 이름 텍스트

    [SerializeField] private Image itemImage;

    [SerializeField] private CanvasGroup resultCanvasGroup;
    public bool result;


    public override void OnClose()
    {
        base.OnClose();
        if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
        {
            CaptureManager.Instance.UnLoadMiniGame();
        }

        else if (SceneManager.GetActiveScene().name == "Ocean_Fishing")
        {
            FishingManager.Instance.UnLoadMiniGame();
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
        {
            result = CaptureManager.Instance.GetResult();
        }
        else if (SceneManager.GetActiveScene().name == "Ocean_Fishing")
        {
            result = FishingManager.Instance.GetResult();
        }
        resultCanvasGroup.DOFade(1f, 1f);
        ShowResultText();

        if (result) // 성공한 경우에만 아이템 정보 표시
        {
            if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
            {
                ShowForestItemInfo();
            }
            else
            {
                ShowOceanItemInfo();
            }
        }

        
    }

    public void ShowResultText()
    {
        if (result)
        {
            successText.gameObject.SetActive(true);
            failText.gameObject.SetActive(false);
            itemNameText.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            successText.gameObject.SetActive(false);
            failText.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);
            itemNameText.gameObject.SetActive(false);
        }
    }

    public async void ShowForestItemInfo()
    {
        int itemKey = CaptureManager.Instance.GetItemKey();
        Data_Foods raw = Data.GetRawItem(itemKey);
        Sprite loadedSprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>(
                $"Assets/16.Image/FoodImage/{raw.englishName}.png", true);
        Debug.Log($"최종 아이템 키 : {itemKey}");
        if (itemKey == 0)
        {
            Debug.Log("포획 실패");
            return;
        }
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = raw.name;

        itemImage.sprite = loadedSprite;
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }

    public async void ShowOceanItemInfo()
    {
        int itemKey = FishingManager.Instance.GetGatheringKey();
        Data_Foods raw = Data.GetRawItem(itemKey);
        Sprite loadedSprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>(
                $"Assets/16.Image/FoodImage/{raw.englishName}.png", true);
        Debug.Log($"최종 아이템 키 : {itemKey}");
        if (itemKey == 0)
        {
            Debug.Log("포획 실패");
            return;
        }
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = raw.name;

        itemImage.sprite = loadedSprite;
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }
}
