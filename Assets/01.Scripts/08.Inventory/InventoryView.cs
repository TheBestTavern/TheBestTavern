using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryView : MonoBehaviour
{
    protected InventoryController controller;
    protected Dictionary<int, List<int>> FoodKeySlotIndex; // <Data_Foods.key, 슬롯 index 리스트>
    protected Dictionary<int, InventorySlot> slots; // <슬롯 index, 슬롯 객체>
    InventoryTrashcan trashcan;

    GameObject slotPref;
    int slotNum; // view가 한번에 보여줄 slot 갯수

    int targetingNum; // 선택 가능한 슬롯 갯수
    protected List<InventorySlot> targetingSlots; // 선택된 슬롯

    [field: Header("toShowType")]
    [SerializeField] public bool Ingredient { get; private set; } = true;// 뷰에서 보여줄 아이템 타입.
    [SerializeField] public bool Processed { get; private set; } = true;// 뷰에서 보여줄 아이템 타입.
    [SerializeField] public bool Dish { get; private set; } = true;// 뷰에서 보여줄 아이템 타입.

    public virtual void 초기화(InventoryController controller)  // 슬롯 딕셔너리 생성, 쓰레기통 생성, 컨트롤러 연결
    {
        //1. 슬롯 딕셔너리 생성
        for (int i = 0; i < slotNum; i++)
        {
            var temp = Instantiate(slotPref).GetComponent<InventorySlot>();
            temp.초기화(i, this);
            slots.Add(i, temp);
        }
        //2. 쓰레기통 생성, 컨트롤러 연결
        this.trashcan = new InventoryTrashcan();
        this.controller = controller;

    }

    public virtual void 아이템띄우기()  // [컨트롤러]의 아이템 띄우기 호출
    {
        controller.아이템띄우기();
    }

    public virtual void 아이템이동() { } // a슬롯의 정보를 b슬롯으로 이동
    public virtual void 아이템버리기() { } // 아이템 버리는 로직 ( 갯수 선택 팝업 )
    public virtual void 아이템정렬_합치기() { } // [컨트롤러]의 정렬_합치기 호출
    public virtual void 아이템정렬_순서() { } // [slots] 정렬_순서 변경

    public virtual void 아이템상세보기() { } //  아이템 상세보기(우클릭)
    public virtual void 아이템타게팅() { } //  아이템 타게팅(좌클릭). 선택 가능한 갯수 초과하면 팝업 호출
    public virtual void 아이템타게팅취소() { } // 아이템 타게팅취소(좌클릭). 타게팅된 슬롯이면 선택 취소.

    public virtual void 아이템툴팁표시() { } // 툴팁 표시
    public virtual void 아이템툴팁표시취소() { } // 툴팁 표시
}
