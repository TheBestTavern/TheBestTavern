using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.EventSystems;

public class InventorySlot : IDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int index;
    InventoryView view;
    bool hasItem;
    Item item;

    public void 초기화(int index, InventoryView view) // UI와 연결, index 부여받기.
    {
        this.index = index;
        this.view = view;
    }

    public void 슬롯세팅() { } // 아이템

    public void OnDrag(PointerEventData eventData) // 아이템 이미지만 마우스 따라서 이동
    {
    }
    public void OnDrop(PointerEventData eventData) // 슬롯에 아이템이 없다면, view 아이템 이동 로직 호출
    {
    }
    public void OnPointerClick(PointerEventData eventData) // 좌클릭 우클릭 구분하여, view 타게팅 또는 상세보기 호출함. 
    {
    }
    public void OnPointerEnter(PointerEventData eventData) // [view] 아이템툴팁표시
    {
    }
    public void OnPointerExit(PointerEventData eventData) // [view] 아이템툴팁표시 취소
    {
    }
}
