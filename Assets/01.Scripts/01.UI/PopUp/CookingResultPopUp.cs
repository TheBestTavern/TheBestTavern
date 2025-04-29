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

    [SerializeField] private CanvasGroup resultCanvasGroup;

    public override void OnClose()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.miniGameUI.OnClickCloseButton();
        base.OnClose();
    }

    public override void OnOpen()
    {
        base.OnOpen();
        resultCanvasGroup.DOFade(1f, 1f);
    }
}
