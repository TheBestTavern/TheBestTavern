using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI_Ingredient : BookUI_Base<IngredientBookSlot, Data_Book_Ingredient>
{
    public override void Init1()
    {
        if (isReady1) return;

        thisBookType = BookType.Ingredient;
        테이블 = DataManager.Instance.DataLoader_Book_Ingredient.ItemsList;
        base.Init1();
    }

    public override void Init2()
    {
        for (int i = 0; i < 한페이지에보이는슬롯수; i++)
        {
            var slot = (IngredientBookSlot)Instantiate(슬롯프리팹, 슬롯생성위치);
            slot.Init();
            슬롯들.Add(slot);
        }
    }
}
