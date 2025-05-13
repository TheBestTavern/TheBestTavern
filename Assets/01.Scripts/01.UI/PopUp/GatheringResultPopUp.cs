using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatheringResultPopUp : BasePopUp
{
    [SerializeField] private TextMeshProUGUI successText; // 성공 텍스트 
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트
    [SerializeField] private TextMeshProUGUI itemNameText; // 아이템 이름 텍스트

    [SerializeField] private Image itemImage;

    [SerializeField] private CanvasGroup resultCanvasGroup;


    public override void OnClose()
    {
        CaptureManager.Instance.UnLoadMiniGame();

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
        bool result = CaptureManager.Instance.GetResult();

        if (result)
        {
            successText.gameObject.SetActive(true);
            failText.gameObject.SetActive(false);
        }
        else
        {
            successText.gameObject.SetActive(false);
            failText.gameObject.SetActive(true);
        }
    }

    public void ShowItemInfo()
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
}
