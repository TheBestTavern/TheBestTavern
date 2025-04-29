using UnityEngine.EventSystems;

public class InventoryTrashcan : IDropHandler
{
    public void 초기화() { } // view 연결
    public void OnDrop(PointerEventData eventData) { }// 아이템 갯수가 2이상이면 버릴 아이템 갯수 입력 팝업 호출
                                                     // => view 아이템 버리기 호출
}
