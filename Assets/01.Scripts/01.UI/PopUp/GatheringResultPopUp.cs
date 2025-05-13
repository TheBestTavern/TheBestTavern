using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
        {
            CaptureManager.Instance.UnLoadMiniGame();
        }

        else if (SceneManager.GetActiveScene().name == "Ocean_Fishing")
        {
            FishingManager.Instance.UnLoadMiniGame();
        }

        base.OnClose();
    }

    public override void OnOpen()
    {
        //setContents();

        resultCanvasGroup.DOFade(1f, 1f);

        

        ShowResultText();

        if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
        {
            ShowForestItemInfo();
        }
        else
        {
            ShowOceanItemInfo();
        }

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
        if (SceneManager.GetActiveScene().name == "Forest_Animal_Dev")
        {
            result = CaptureManager.Instance.GetResult();
        }
        else if (SceneManager.GetActiveScene().name == "Ocean_Fishing")
        {
            result = FishingManager.Instance.GetResult();
        }

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
            itemNameText.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(false);
        }
    }

    public void ShowForestItemInfo()
    {

        int itemKey = CaptureManager.Instance.GetItemKey();
        Debug.Log($"최종 아이템 키 : {itemKey}");
        if (itemKey == -1)
        {
            Debug.Log("포획 실패");
            itemNameText.gameObject.SetActive(false);
            return;
        }
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = data.name;

        itemImage.sprite = Resources.Load<Sprite>($"Item/{data.englishName}");
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }

    public void ShowOceanItemInfo()
    {
        int itemKey = FishingManager.Instance.GetGatheringKey();
        Debug.Log($"최종 아이템 키 : {itemKey}");
        if (itemKey == -1)
        {
            Debug.Log("포획 실패");
            itemNameText.gameObject.SetActive(false);
            return;
        }
        var data = DataManager.Instance.DataLoader_Foods.GetByKey(itemKey);
        itemNameText.text = data.name;

        itemImage.sprite = Resources.Load<Sprite>($"Item/{data.englishName}");
        if (itemImage.sprite == null) { itemImage.gameObject.SetActive(false); }
    }
}
