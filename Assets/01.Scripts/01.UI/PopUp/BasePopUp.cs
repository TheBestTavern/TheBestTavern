using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 베이스 팝업 클래스
/// </summary>
public class BasePopUp : MonoBehaviour
{
    // 닫기 버튼 
    [SerializeField] protected Button closeButton;

    // 팝업 타입 
    public PopUpType popUpType;

    public virtual void Awake()
    {
        // 닫기 버튼 이벤트 리스너 추가
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    // 닫기 버튼 함수
    public virtual void OnClickCloseButton()
    {
        OnClose();
    }

    // 팝업 열때 필요한 함수
    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }

    // 팝업 닫을 때 필요한 함수
    public virtual void OnClose()
    {

    }
}
