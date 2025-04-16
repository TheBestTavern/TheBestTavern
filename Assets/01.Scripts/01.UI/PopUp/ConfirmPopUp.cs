using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 확인 팝업 클래스
/// </summary>
public class ConfirmPopUp : BasePopUp
{
    // 확인 버튼
    [SerializeField] private Button confirmButton;

    // 확인 텍스트
    [SerializeField] private TextMeshProUGUI confirmText;

    // 확인 액션
    public Action confirmAction;

    public override void Awake()
    {
        base.Awake();
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }

    // 확인 버튼 클릭 함수
    void OnClickConfirmButton()
    {
        // 확인 액션 실행
        confirmAction?.Invoke();
    }

    /// <summary>
    /// 확인 팝업 설정 함수
    /// </summary>
    /// <param name="text">확인 텍스트 넣기 (Ex : 정말 이동하시겠습니까?)</param>
    /// <param name="action">확인 액션 넣기 (Ex : 씬 이동 함수)</param>
    public void SetConfirm(string text, Action action)
    {
        // 확인 텍스트 설정 
        confirmText.text = text;

        // 확인 액션 설정
        confirmAction = action;
    }

    // 팝업 닫을 때 필요한 함수
    public override void OnClose()
    {
        base.OnClose();
        gameObject.SetActive(false);
    }
}
