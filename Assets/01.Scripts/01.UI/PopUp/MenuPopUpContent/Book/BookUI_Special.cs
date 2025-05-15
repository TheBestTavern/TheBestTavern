using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI_special : BookUI_Base<SpecialBookSlot, Data_Book_Special>
{
    public override void Init1(BookUI bookUI)
    {
        if (isReady1) return;

        thisBookType = BookType.Special;
        테이블 = DataManager.Instance.DataLoader_Book_Special.ItemsList;
        base.Init1(bookUI);
    }

    public override void Init2()
    {
        base.Init2();
        //for (int i = 0; i < 한페이지에보이는슬롯수; i++)
        //{
        //    var slot = (SpecialBookSlot)Instantiate(슬롯프리팹, 슬롯생성위치);
        //    slot.Init();
        //    슬롯들.Add(slot);
        //}
    }
}
