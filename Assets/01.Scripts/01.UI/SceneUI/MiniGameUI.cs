using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 미니게임 UI
/// </summary>
public class MiniGameUI : MonoBehaviour
{
    // 미니게임 닫기 버튼 
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        // 미니게임 닫기 버튼 클릭 이벤트 리스너 추가 
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    // 미니게임 닫기 버튼 함수 
    public void OnClickCloseButton()
    {
        // 미니게임 닫기 
        CookingMiniGameManager.Instance.CloseMiniGame();
    }
}
