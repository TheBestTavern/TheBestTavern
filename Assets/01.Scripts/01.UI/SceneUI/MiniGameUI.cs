using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 미니게임 UI
/// </summary>
public class MiniGameUI : MonoBehaviour
{
    // 미니게임 닫기 버튼 
    [SerializeField] private Button closeButton;

    // 미니게임 타이머
    [SerializeField] private Image timerImage;

    private void Awake()
    {
        UIManager.Instance.miniGameUI = this;

        // 미니게임 닫기 버튼 클릭 이벤트 리스너 추가 
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    // 미니게임 닫기 버튼 함수 
    public void OnClickCloseButton()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.CloseMiniGame();
    }

    // 타이머 업데이트 (남은시각, 게임 자체 제한시간)
    public void UpdateTimer(float playTime)
    {
        timerImage.fillAmount = playTime / 15f;
    }

    public void ResetTimer()
    {
        timerImage.fillAmount = 0;
    }
}
