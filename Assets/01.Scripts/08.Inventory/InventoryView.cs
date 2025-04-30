using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryView : MonoBehaviour
{
    [field: SerializeField] public InvenType invenType { get; private set; }
    public bool IsInitialized { get; private set; }
    protected InventoryController controller;
    protected Dictionary<int, InventorySlot> index2Slots = new(); // <슬롯 index, 슬롯 객체>
    //protected Dictionary<int, int> ID2SlotIndex; // <ID, 슬롯 index>
    protected BiDictionary<int, int> BiID2SlotIndex = new(); // <ID, 슬롯 index> 양방향 딕셔너리 클래스
    //protected Dictionary<int, List<int>> FoodKeySlotIndex; // <Data_Foods.key, 슬롯 index 리스트>
    InventoryTrashcan trashcan;

    [SerializeField] protected InventorySlot slotPref;
    [SerializeField] protected Transform slotTrs;
    [SerializeField] protected int slotCount; // 슬롯갯수 (OR view가 한번에 보여줄 slot 갯수)


    [field: Header("toShowType")]
    [field: SerializeField] public List<DesignEnums.ItemType> toShowTypes { get; private set; }// 뷰에서 보여줄 아이템 타입.

    private void Awake()
    {
        초기화BySelf();
    }

    public virtual void 초기화ByController(InventoryController controller)
    {
        if (IsInitialized) return;
        IsInitialized = true;

        초기화(controller);
        아이템띄우기();
    }

    public virtual void 초기화BySelf() // 컨트롤러를 직접 찾아서 초기화.
    {
        if (IsInitialized) return;
        if (InventoryManager.Instance.Invens.TryGetValue(invenType, out InventoryController controller))
        {
            if (IsInitialized) return;
            IsInitialized = true;

            초기화(controller);
            아이템띄우기();
        }
        else
        {
            return;
        }
    }

    protected virtual void 초기화(InventoryController controller)// 슬롯 딕셔너리 생성, 쓰레기통 생성, 컨트롤러 연결, 슬롯 갯수 
    {
        //1. 쓰레기통 생성, 컨트롤러 연결, 슬롯 갯수 
        this.trashcan = new InventoryTrashcan();
        this.controller = controller;
        controller.AddView(this);

        //2. 슬롯 딕셔너리 생성
        for (int i = 0; i < this.slotCount; i++)
        {
            var temp = Instantiate(slotPref, slotTrs);
            //temp.transform.SetParent(slotTrs);
            temp.초기화(i, this, 슬롯비우기, 아이템타게팅, 아이템타게팅취소);
            index2Slots.Add(i, temp);
        }
    }

    public virtual void 아이템띄우기()  // 전체 아이템 띄우기
    {
        int targetIndex = 0;
        foreach (var pair in controller.모델정보반환())
        {
            if (!toShowTypes.Contains(ItemStackManager.Instance.AllItemStack[pair.Key].Origin.itemCategory)) continue;

            index2Slots[targetIndex].슬롯세팅(pair.Key);
            BiID2SlotIndex.Add(pair.Key, targetIndex);
            targetIndex++;
        }
    }

    public virtual void 특정아이템정보갱신(int id)  // 특정 ID의 정보만 갱신
    {
        if (!toShowTypes.Contains(ItemStackManager.Instance.AllItemStack[id].Origin.itemCategory)) return;

        if (BiID2SlotIndex.ContainsKey(id))
        {
            // 있으면 해당 슬롯 정보 갱신
            index2Slots[BiID2SlotIndex.GetByKey(id)].슬롯갱신();
        }
        else
        {
            // 없으면 새로운 슬롯에 배정
            for (int i = 0; i < slotCount; i++)
            {
                if (!index2Slots[i].HasItem)
                {
                    index2Slots[i].슬롯세팅(id);
                    BiID2SlotIndex.Add(id, i);
                    break;
                }
            }
        }
    }

    public virtual void 슬롯비우기(int index)
    {
        BiID2SlotIndex.RemoveByValue(index);
    }


    public virtual void 아이템타게팅(int index) // loose에서
    {
    }
    public virtual void 아이템타게팅취소(int index) // loose에서
    {
    }

    //공통
    public virtual void 아이템정렬_합치기() { } // [컨트롤러]의 정렬_합치기 호출
    public virtual void 아이템정렬_순서() { } // [slots] 정렬_순서 변경
    public virtual void 아이템상세보기() { } //  아이템 상세보기(우클릭)
    public virtual void 아이템툴팁표시() { } // 툴팁 표시
    public virtual void 아이템툴팁표시취소() { } // 툴팁 표시
}
