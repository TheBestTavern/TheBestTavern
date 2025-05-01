using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 베이스 팝업 클래스
/// </summary>
public class BasePopUp : DraggableMonoBehaviour, IPointerDownHandler
{
    // 닫기 버튼 
    [SerializeField] protected Button closeButton;
    [SerializeField] protected Canvas canvas;
    int id = -1;
    IPopupManager popupManager;
    // 팝업 타입 
    public PopUpType popUpType;

    public virtual void Init(int id, IPopupManager manager)
    {
        //canvas = GetComponent<Canvas>();
        if (canvas == null) { canvas = GetComponentInChildren<Canvas>(); }
        this.id = id;
        this.popupManager = manager;
    }

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

    public void SetSortingOrder()
    {
        canvas.sortingOrder = popupManager.GetNextSortingOrder();
    }

    // 팝업 열때 필요한 함수
    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
        popupManager.PopupOpen(id);
        SetSortingOrder();
    }

    // 팝업 닫을 때 필요한 함수
    public virtual void OnClose()
    {
        popupManager.PopupClose(id);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        popupManager.RecoverID(id);
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
         canvas.sortingOrder = popupManager.GetNextSortingOrder();
    }
}
