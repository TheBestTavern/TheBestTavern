using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryView : MonoBehaviour
{
    [field: SerializeField] public InvenType invenType { get; private set; }
    public bool IsInitialized { get; private set; }
    protected InventoryController controller;
    protected Dictionary<int, InventorySlot> index2Slots = new(); // <슬롯 index, 슬롯 객체>
    protected BiDictionary<int, int> BiID2SlotIndex = new(); // <ID, 슬롯 index> 양방향 딕셔너리 클래스

    [SerializeField] protected InventorySlot slotPref;
    [SerializeField] protected Transform slotTrs;
    [SerializeField] protected int slotCount; // 슬롯갯수 (OR view가 한번에 보여줄 slot 갯수)


    [field: Header("toShowType")]
    [field: SerializeField] public List<DesignEnums.ItemType> toShowTypes { get; private set; }// 뷰에서 보여줄 아이템 타입.

    //public virtual void InitailizeByController(InventoryController controller)
    //{
    //    if (IsInitialized) return;
    //    IsInitialized = true;

    //    Initialize(controller);
    //    ViewAllItems();
    //}

    //public virtual void InitializeBySelf() // 컨트롤러를 직접 찾아서 초기화.
    //{
    //    if (IsInitialized) return;
    //    if (InventoryManager.Instance.Invens.TryGetValue(invenType, out InventoryController controller))
    //    {
    //        if (IsInitialized) return;
    //        IsInitialized = true;

    //        Initialize(controller);
    //        ViewAllItems();
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    private void Start()
    {
        if (InventoryManager.Instance.Invens.TryGetValue(invenType, out InventoryController _controller))
        {
            Initialize(_controller);
            ViewAllItems();
        }
    }

    protected virtual void Initialize(InventoryController _controller)// 슬롯 딕셔너리 생성, 쓰레기통 생성, 컨트롤러 연결, 슬롯 갯수 
    {
        //1. 컨트롤러 연결, 슬롯 갯수 
        this.controller = _controller;
        controller.RegisterView(this);

        //2. 슬롯 딕셔너리 생성
        for (int i = 0; i < this.slotCount; i++)
        {
            var temp = Instantiate(slotPref, slotTrs);
            //temp.transform.SetParent(slotTrs);
            temp.Init(i, this, EmptifySlot, TargetingSlot, TargetingSlotCancel);
            index2Slots.Add(i, temp);
        }
    }

    public virtual void ViewAllItems()  // 전체 아이템 띄우기
    {
        int targetIndex = 0;
        foreach (var itemStackID in controller.GetModel())
        {
            if (!toShowTypes.Contains(Data.GetRawItemFromItemStack(itemStackID).itemCategory)) continue;

            index2Slots[targetIndex].SetSlot(itemStackID);
            BiID2SlotIndex.Add(itemStackID, targetIndex);
            targetIndex++;
        }
    }

    public void MoveItem(int toSlotIndex, int fromSlotIndex)
    {
        var toSlot = index2Slots[toSlotIndex];
        var fromSlot = index2Slots[fromSlotIndex];

        if (!toSlot.HasItem && fromSlot != null && fromSlot != this && fromSlot.HasItem)
        {
            int tempItemStackID = index2Slots[fromSlotIndex].GetSlotItemStackID();
            // from 슬롯 비우기
            fromSlot.EmptifySlot();

            // to 슬롯에 채우기
            toSlot.SetSlot(tempItemStackID);
            BiID2SlotIndex.Add(tempItemStackID, toSlotIndex);
        }
    }


    public virtual void ReviewSpecificItemStack(int id)  // 특정 ID의 정보만 갱신
    {
        if (!toShowTypes.Contains(Data.GetRawItemFromItemStack(id).itemCategory)) return;

        if (BiID2SlotIndex.ContainsKey(id))
        {
            // 있으면 해당 슬롯 정보 갱신
            index2Slots[BiID2SlotIndex.GetByKey(id)].ReviewSlot();
        }
        else
        {
            // 없으면 새로운 슬롯에 배정
            for (int i = 0; i < slotCount; i++)
            {
                if (!index2Slots[i].HasItem)
                {
                    index2Slots[i].SetSlot(id);
                    BiID2SlotIndex.Add(id, i);
                    break;
                }
            }
        }
    }

    public virtual void EmptifySlot(int index)
    {
        BiID2SlotIndex.RemoveByValue(index);
    }


    public virtual void TargetingSlot(int index) // loose에서
    {
    }
    public virtual void TargetingSlotCancel(int index) // loose에서
    {
    }

    //공통
    //public virtual void SortingView_Merge() { } // [컨트롤러]의 정렬_합치기 호출
    //public virtual void SortingView_Order() { } // [slots] 정렬_순서 변경
    //public virtual void OpenItemDetails() { } //  아이템 상세보기(우클릭)
}
