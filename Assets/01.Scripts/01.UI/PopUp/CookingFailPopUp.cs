using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CookingFailPopUp : BasePopUp
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI failText; // 실패 텍스트


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

    public void ShowInfo()
    {
        failText.text = "조리 도구를 잘못 선택한 것 같다...";
        failText.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFX("Fail");
    }
}
