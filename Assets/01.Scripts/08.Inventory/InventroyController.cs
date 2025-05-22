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
        model.Init(invenType, slotCount, maxStackSize, ViewSpecificItem);

        var allViews = InventoryManager.Instance.FindInventoryView();
        OnAfterSceneMove();
    }

    public virtual void OnBeforeSceneMove() // view 찾아서 연결. 씬이동할때마다 실행? 안해도 view에서 자체적으로 연결을 걸듯. 
    {
        views.Clear();
    }

    public virtual void OnAfterSceneMove() // view 찾아서 연결. 씬이동할때마다 실행? 안해도 view에서 자체적으로 연결을 걸듯. 
    {
        var allViews = InventoryManager.Instance.FindInventoryView();

        foreach (var view in allViews)
        {
            if (view.invenType == invenType)
            {
                if (views.Contains(view)) continue;

                view.InitailizeByController(this);
            }
        }
    }

    public void AddView(InventoryView view)
    {
        views.Add(view);
    }

    public virtual bool AcquireItem(Data_Foods data_Foods, int amount)
    {
        if (!model.AddItemWithCheck(data_Foods, amount))
        {
            return false;
        }
        return true;
    }

    public bool AcquireItem(int itemID, int amount)
    {
        var data_Foods = Data.GetRawItem(itemID);
        return AcquireItem(data_Foods, amount);
    }

    public virtual bool LooseItem(Data_Foods data_Foods, int amount)
    {
        if (!model.DecreaseItemWithCheck(data_Foods, amount))
        {
            return false;
        }
        return true;
    }

    public bool ThrowInTrash(Data_Foods data_Foods, int amount)
    {
        if (amount > 0)
        {
            return LooseItem(data_Foods, amount);
        }
        else
        {
            return false;
        }
    }

    public void SortingModel_Merge()
    {
        model.SortingModel_Merge();
    }

    public Dictionary<int, ItemStack> GetModel()
    {
        return model.ID2ItemStack;
    }

    public void ViewSpecificItem(int id, InvenType itemStackInvenType)
    {
        foreach (var view in views)
        {
            if (view.invenType == invenType && itemStackInvenType == invenType)
            {
                view.ReviewSpecificItemStack(id);
            }
        }
    }

    public void Dispose()
    {
        model.Dipose();
    }
}
