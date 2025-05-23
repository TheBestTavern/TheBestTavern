using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingFailPopUp : BasePopUp
{
    [SerializeField] private TextMeshProUGUI itemNameText; // 아이템 이름 텍스트
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트


    public override void OnClose()
    {
        // 미니게임 닫기 
        //CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();
        canvasGroup?.DOKill();
        base.OnClose();
    }

    public override void OnOpen()
    {
        if (canvasGroup != null)
        {   canvasGroup.DOFade(1f, 1f); }

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
    public void ShowInfo()
    {
            // 조리 도구 실패
            itemNameText.text = "조리 도구를 잘못 선택한 것 같다...";
            failText.gameObject.SetActive(true);
            itemNameText.gameObject.SetActive(true);
            SoundManager.Instance.PlaySFX("Fail");
    }
}
    
