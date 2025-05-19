using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController
{
    InvenType invenType;

    public InventoryModel model { get; private set; }
    protected List<InventoryView> views = new();

    public int slotCount { get; private set; }
    public int maxStackSize { get; private set; }

    public virtual void Init(InvenType invenType, int slotCount, int maxStackSize) // 모델, 뷰 생성.
    {
        this.invenType = invenType;
        this.slotCount = slotCount;
        this.maxStackSize = maxStackSize;

        this.model = new InventoryModel();
        model.Init(slotCount, maxStackSize, 특정아이템정보변경);

        var allViews = InventoryManager.Instance.FindInventoryView();
        On씬이동After();
    }

    public virtual void On씬이동Before() // view 찾아서 연결. 씬이동할때마다 실행? 안해도 view에서 자체적으로 연결을 걸듯. 
    {
        views.Clear();
    }

    public virtual void On씬이동After() // view 찾아서 연결. 씬이동할때마다 실행? 안해도 view에서 자체적으로 연결을 걸듯. 
    {
        var allViews = InventoryManager.Instance.FindInventoryView();

        foreach (var view in allViews)
        {
            if (view.invenType == invenType)
            {
                if (views.Contains(view)) continue;

                view.초기화ByController(this);
            }
        }
    }

    public void AddView(InventoryView view)
    {
        views.Add(view);
    }

    public virtual bool 아이템획득(Data_Foods data_Foods, int amount)
    {
        if (!model.아이템검사후추가(data_Foods, amount))
        {
            return false;
        }
        return true;
    }

    public virtual bool 아이템잃음(Data_Foods data_Foods, int amount)
    {
        if (!model.아이템감소(data_Foods, amount))
        {
            return false;
        }
        return true;
    }

    public void 아이템정렬_합치기()
    {
        model.아이템정렬_합치기();
    }

    public Dictionary<int, ItemStack> 모델정보반환()
    {
        return model.ID2ItemStack;
    }

    public void 특정아이템정보변경(int id)
    {
        foreach (var view in views)
        {
            view.특정아이템정보갱신(id);
        }
    }
}
