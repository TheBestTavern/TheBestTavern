using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int index;
    InventoryView view;

    public bool HasItem { get; private set; }
    int ID;
    ItemStack item;
    int count;
    public Image image;
    [SerializeField] TextMeshProUGUI CountTMP;
    public bool IsTargeting { get; private set; }

    private GameObject draggingIcon;
    private RectTransform draggingIconTransform;
    private Canvas rootCanvas;

    Action<int> OnZero;
    Action<int> OnClick;
    Action<int> OnClickAgain;

    public void 초기화(int index, InventoryView view, Action<int> removeItem, Action<int> clickSlot, Action<int> clickSlotAgain) // UI와 연결, index 부여받기.
    {
        this.index = index;
        this.view = view;

        image.sprite = null;
        image.gameObject.SetActive(false);
        HasItem = false;
        item = null;
        ID = -1;
        count = -1;
        CountTMP.text = "";

        OnZero = removeItem;
        OnClick = clickSlot;
        OnClickAgain = clickSlotAgain;
    }

    public async void 슬롯세팅(int id)  // 아이템 ( Item주입, 이미지, 수량, bool hasItem 변동 )
    {
        HasItem = true;
        this.ID = id;
        item = ItemStackManager.Instance.AllItemStack[id];
        count = item.Count;
        CountTMP.text = count.ToString();
        Data_Foods raw = Data.GetRawItem(item.Origin.key);
        image.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{raw.englishName}.png", true);
        image.gameObject.SetActive(true);
    }

    public void 슬롯갱신()
    {
        count = item == null ? 0 : item.Count;

        CountTMP.text = count > 0 ? count.ToString() : "";

        if (count == 0)
        {
            슬롯비우기();
        }
    }

    public void 슬롯비우기()
    {
        HasItem = false;
        item = null;
        image.sprite = null;
        image.gameObject.SetActive(false);
        ID = -1;
        count = -1;
        CountTMP.text = " "; //CountTMP 초기화
        OnZero?.Invoke(index);
    }

    private void TriggerClickAction() => OnClick?.Invoke(index);
    private void TriggerClickAgainAction() => OnClickAgain?.Invoke(index);

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!HasItem) return;

        rootCanvas = GetComponentInParent<Canvas>();

        draggingIcon = new GameObject("DraggingIcon");
        draggingIconTransform = draggingIcon.AddComponent<RectTransform>();
        draggingIconTransform.SetParent(rootCanvas.transform, false);
        draggingIconTransform.sizeDelta = new Vector2(64, 64);

        Image iconImage = draggingIcon.AddComponent<Image>();
        iconImage.sprite = image.sprite;
        iconImage.raycastTarget = false;
        draggingIconTransform.position = eventData.position;

        eventData.pointerDrag = this.gameObject;
    }

    public void OnDrag(PointerEventData eventData) // 아이템 이미지만 마우스 따라서 이동
    {
        if (draggingIconTransform != null)
        {
            draggingIconTransform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData) // 슬롯에 아이템이 없다면, view 아이템 이동 로직 호출
    {
        InventorySlot fromSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();

        view.아이템이동(index, fromSlot.index);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingIcon != null)
        {
            Destroy(draggingIcon);
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            
        }
    }

    public void OnPointerClick(PointerEventData eventData) // 좌클릭 우클릭 구분하여, view 타게팅 또는 상세보기 호출함. 
    {
        if (HasItem)
        {
            if (!IsTargeting)
            {
                TriggerClickAction();
                EnterTargetingState();
            }
            else
            {
                TriggerClickAgainAction();
                ExitTargetingState();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // [view] 아이템툴팁표시
    {
        if (HasItem)
        {
            EventBus.Publish<SlotHoverEnterEvent>(new SlotHoverEnterEvent(item.Origin.key));
            Debug.Log("들어옴");
        }
    }

    public void OnPointerExit(PointerEventData eventData) // [view] 아이템툴팁표시 취소
    {
        if (HasItem)
        {
            Debug.Log("나감");
            EventBus.Publish<SlotHoverEndEvent>(new SlotHoverEndEvent());
        }
    }

    private void EnterTargetingState()
    {
        IsTargeting = true;
        image.color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }

    public void ExitTargetingState()
    {
        IsTargeting = false;
        image.color = Color.white;
    }

    public ItemStack GetSlotItem()
    {
        return item;
    }

    //private void OnDisable()
    //{
    //    if (IsTargeting)
    //    {
    //        TriggerClickAgainAction();
    //        ExitTargetingState();
    //    }
    //}
}
