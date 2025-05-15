using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IBook
{
    public BookType thisBookType { get; set; }
    public void Init1(BookUI bookUI);
    public void Init2();
    public void On();
    public void Off();
}

public abstract class BookUI_Base<TSlot, TData> : MonoBehaviour, IBook where TSlot : BaseBookSlot<TData>
{
    public BookType thisBookType { get; set; }
    BookUI bookUI;

    protected int slotCountPerPage = 6;
    protected int curPage;
    protected int lastPage;

    [SerializeField] protected List<TSlot> slots;
    [SerializeField] protected BaseBookSlot<TData> slotPref;
    [SerializeField] protected Transform slotTsr;

    protected List<TData> bookTable; // 요소 타입 바꿔야

    [SerializeField] Button nextBtn;
    [SerializeField] Button PrevBtn;
    [SerializeField] TextMeshProUGUI pageUI;

    protected bool isReady1;
    protected bool isReady2;

    [SerializeField] protected bool HideUndiscoveredFood = true;

    public virtual void Init1(BookUI bookUI)
    {
        this.bookUI = bookUI;
        nextBtn.onClick.AddListener(() =>
        {
            if (lastPage == 0) return;
            GoNextPage();
            ReviewPageUI();
            ReviewSlotsByCurPage();
        });
        PrevBtn.onClick.AddListener(() =>
        {
            if (lastPage == 0) return;
            GoPrevPage();
            ReviewPageUI();
            ReviewSlotsByCurPage();
        });

        lastPage = (bookTable.Count - 1) / slotCountPerPage + 1;

        if (lastPage == 0)
        {
            curPage = 0;
        }
        else
        {
            curPage = 1;
        }

        isReady1 = true;
    }

    public virtual void Init2()
    {
        for (int i = 0; i < slotCountPerPage; i++)
        {
            var slot = (TSlot)Instantiate(slotPref, slotTsr);
            slot.Init(bookUI);
            slots.Add(slot);
        }
        isReady2 = true;
    }

    public void On()
    {
        if (!isReady2) Init2();

        gameObject.SetActive(true);
        ReviewSlotsByCurPage();
        ReviewPageUI();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }


    public void ReviewSlotsByCurPage()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int itemIndex = slotCountPerPage * (curPage - 1) + i;
            if (itemIndex < bookTable.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].SetSlot(bookTable[itemIndex]);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    public void GoNextPage()
    {
        if (curPage < lastPage)
        {
            curPage++;
        }
        else
        {
            curPage = 1;
        }
    }

    public void GoPrevPage()
    {
        if (1 < curPage)
        {
            curPage--;
        }
        else
        {
            curPage = lastPage;
        }
    }

    public void ReviewPageUI()
    {
        pageUI.text = $"{curPage} / {lastPage}";
    }
}