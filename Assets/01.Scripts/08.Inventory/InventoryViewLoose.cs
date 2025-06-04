using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewLoose : InventoryView
{
    [SerializeField] Button nextPageBtn;
    [SerializeField] Button prevPageBtn;
    [SerializeField] int showSlotCount;
    [SerializeField] TextMeshProUGUI PagesTMP;

    int currentPage; // 시작페이지 = 0
    int lastPage;
    Queue<int> showingSlotIndex = new();

    [SerializeField] protected int minTargetingNum = 1; // 어떤 행동을 하기 위해 최소로 선택해야하는 슬롯 갯수
    [SerializeField] protected int maxTargetingNum = 1; // 최대 선택 가능한 슬롯 갯수
    protected List<int> targetingSlots = new(); // 선택된 슬롯
    public Action<List<Data_Foods>> OnEnableTargetSlot;
    public Action OnDisalbeTargetSlot;

    protected virtual void OnEnable()
    {
        //InitializeBySelf();
    }

    private void 아이템있는슬롯만보여주기()
    {
        int itemCount = Bi_StackID2SlotIndex.Count;
        lastPage = (itemCount == 0) ? 0 : (itemCount - 1) / showSlotCount;

        if (currentPage > lastPage)
        {
            currentPage = lastPage;
        }

        int temp = currentPage * showSlotCount;
        int showCount = showSlotCount;

        while (showingSlotIndex.Count > 0)
        {
            index2Slots[showingSlotIndex.Dequeue()].gameObject.SetActive(false);
        }

        for (int i = 0; i < slotCount && showCount > 0; i++)
        {
            if (index2Slots[i].HasItem)
            {
                temp--;
                if (temp < 0)
                {
                    index2Slots[i].gameObject.SetActive(true);
                    showingSlotIndex.Enqueue(i);
                    showCount--;
                }
            }
        }

        PagesTMP.text = $"{currentPage + 1} / {lastPage + 1}";
    }

    protected override void Initialize(InventoryController controller)
    {
        base.Initialize(controller);

        nextPageBtn.onClick.AddListener(NextPage);
        prevPageBtn.onClick.AddListener(PrevPage);

        foreach (var pair in index2Slots)
        {
            pair.Value.gameObject.SetActive(false);
        }
    }

    private void NextPage()
    {
        currentPage++;
        if (currentPage > lastPage)
        {
            currentPage = 0;
        }
        아이템있는슬롯만보여주기();
    }

    private void PrevPage()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = lastPage;
        }
        아이템있는슬롯만보여주기();
    }

    public override void ReviewSpecificItemStack(int id)  // 특정 ID의 정보만 갱신
    {
        base.ReviewSpecificItemStack(id);

        아이템있는슬롯만보여주기();
    }

    public override void ViewAllItems()  // 전체 아이템 띄우기
    {
        base.ViewAllItems();

        아이템있는슬롯만보여주기();
    }

    public override void EmptifySlot(int index)
    {
        base.EmptifySlot(index);

        아이템있는슬롯만보여주기();
    }

    public override void TargetingSlot(int index)  //  아이템 타게팅(좌클릭). 
    {
        base.TargetingSlot(index);

        while (targetingSlots.Count >= maxTargetingNum)
        {
            index2Slots[targetingSlots[0]].ExitTargetingState();
            targetingSlots.RemoveAt(0);
        }

        targetingSlots.Add(index);
        TriggerOnTargetSlot();
    }
    public override void TargetingSlotCancel(int index) // 아이템 타게팅취소(좌클릭). 타게팅된 슬롯이면 선택 취소.
    {
        base.TargetingSlotCancel(index);

        targetingSlots.Remove(index);
        TriggerOnTargetSlot();
    }

    protected virtual void TriggerOnTargetSlot()
    {
        if (!(targetingSlots.Count >= minTargetingNum && targetingSlots.Count <= maxTargetingNum))
        {
            OnDisalbeTargetSlot?.Invoke();
        }
        else
        {
            List<Data_Foods> rawItems = new();
            for (int i = 0; i < targetingSlots.Count; i++)
            {
                rawItems.Add(Data.GetRawItemFromItemStack(index2Slots[targetingSlots[i]].GetSlotItemStackID()));
            }
            OnEnableTargetSlot?.Invoke(rawItems);
        }
    }

    private void OnDisable()
    {
        foreach (var index in targetingSlots)
        {
            index2Slots[index].ExitTargetingState();
            OnDisalbeTargetSlot?.Invoke();
        }
        targetingSlots.Clear();
    }
}