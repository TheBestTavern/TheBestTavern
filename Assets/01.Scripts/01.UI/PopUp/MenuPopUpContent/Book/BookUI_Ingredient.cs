using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI_Ingredient : BookUI_Base<IngredientBookSlot, Data_Book_Ingredient>
{
    public override void Init1(BookUI bookUI)
    {
        if (isReady1) return;

        thisBookType = BookType.Ingredient;
        bookTable = DataManager.Instance.DataLoader_Book_Ingredient.ItemsList;
        base.Init1(bookUI);
    }

    public override void Init2()
    {
        base.Init2();

        //for (int i = 0; i < 한페이지에보이는슬롯수; i++)
        //{
        //    var slot = (IngredientBookSlot)Instantiate(슬롯프리팹, 슬롯생성위치);
        //    slot.Init();
        //    슬롯들.Add(slot);
        //}
    }
}
