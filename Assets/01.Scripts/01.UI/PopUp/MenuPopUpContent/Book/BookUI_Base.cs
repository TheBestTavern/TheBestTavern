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

    protected int 한페이지에보이는슬롯수 = 6;
    protected int 현재페이지;
    protected int 마지막페이지;

    [SerializeField] protected List<TSlot> 슬롯들;
    [SerializeField] protected BaseBookSlot<TData> 슬롯프리팹;
    [SerializeField] protected Transform 슬롯생성위치;

    protected List<TData> 테이블; // 요소 타입 바꿔야

    [SerializeField] Button 다음버튼;
    [SerializeField] Button 이전버튼;
    [SerializeField] TextMeshProUGUI pageUI;

    protected bool isReady1;
    protected bool isReady2;

    public virtual void Init1(BookUI bookUI)
    {
        this.bookUI = bookUI;
        다음버튼.onClick.AddListener(() =>
        {
            if (마지막페이지 == 0) return;
            다음페이지();
            페이지UI갱신();
            현재페이지에맞게슬롯갱신();
        });
        이전버튼.onClick.AddListener(() =>
        {
            if (마지막페이지 == 0) return;
            이전페이지();
            페이지UI갱신();
            현재페이지에맞게슬롯갱신();
        });

        마지막페이지 = (테이블.Count - 1) / 한페이지에보이는슬롯수 + 1;

        if (마지막페이지 == 0)
        {
            현재페이지 = 0;
        }
        else
        {
            현재페이지 = 1;
        }

        isReady1 = true;
    }

    public virtual void Init2()
    {
        for (int i = 0; i < 한페이지에보이는슬롯수; i++)
        {
            var slot = (TSlot)Instantiate(슬롯프리팹, 슬롯생성위치);
            slot.Init(bookUI);
            슬롯들.Add(slot);
        }
        isReady2 = true;
    }

    public void On()
    {
        if (!isReady2) Init2();

        gameObject.SetActive(true);
        현재페이지에맞게슬롯갱신();
        페이지UI갱신();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }


    public void 현재페이지에맞게슬롯갱신()
    {
        for (int i = 0; i < 슬롯들.Count; i++)
        {
            int 인덱스 = 한페이지에보이는슬롯수 * (현재페이지 - 1) + i;
            if (인덱스 < 테이블.Count)
            {
                슬롯들[i].gameObject.SetActive(true);
                슬롯들[i].SetSlot(테이블[인덱스]);
            }
            else
            {
                슬롯들[i].gameObject.SetActive(false);
            }
        }
    }

    public void 다음페이지()
    {
        if (현재페이지 < 마지막페이지)
        {
            현재페이지++;
        }
        else
        {
            현재페이지 = 1;
        }
    }

    public void 이전페이지()
    {
        if (1 < 현재페이지)
        {
            현재페이지--;
        }
        else
        {
            현재페이지 = 마지막페이지;
        }
    }

    public void 페이지UI갱신()
    {
        pageUI.text = $"{현재페이지} / {마지막페이지}";
    }
}