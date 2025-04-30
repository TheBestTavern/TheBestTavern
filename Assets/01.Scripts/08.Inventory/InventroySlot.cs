using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void 슬롯세팅(int id)  // 아이템 ( Item주입, 이미지, 수량, bool hasItem 변동 )
    {
        HasItem = true;
        this.ID = id;
        item = ItemStackManager.Instance.AllItemStack[id];
        count = item.Count;
        CountTMP.text = count.ToString();
        Data_Foods raw = Data.GetRawItem(item.Origin.key);
        image.sprite = Resources.Load<Sprite>($"Item/{item.Origin.englishName}");
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

    private void 슬롯비우기()
    {
        HasItem = false;
        item = null;
        image.sprite = null;
        image.gameObject.SetActive(false);
        ID = -1;
        count = -1;
        OnZero?.Invoke(index);
    }

    private void TriggerClickAction() => OnClick?.Invoke(index);
    private void TriggerClickAgainAction() => OnClickAgain?.Invoke(index);

    public void OnDrag(PointerEventData eventData) // 아이템 이미지만 마우스 따라서 이동
    {
    }
    public void OnDrop(PointerEventData eventData) // 슬롯에 아이템이 없다면, view 아이템 이동 로직 호출
    {
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
    }
    public void OnPointerExit(PointerEventData eventData) // [view] 아이템툴팁표시 취소
    {
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
}
