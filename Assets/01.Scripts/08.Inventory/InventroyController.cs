using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryController : MonoBehaviour
{
    protected InventoryModel model;
    protected List<InventoryView> views;

    [SerializeField] int slotMaxCount;
    [SerializeField] int perStackMaxCount;


    public virtual void Init() // 모델, 뷰 생성.
    {
        this.model = new InventoryModel();
        model.Init(slotMaxCount, perStackMaxCount, 특정아이템정보변경);
        DontDestroyOnLoad(gameObject);
        On씬이동();
    }

    public virtual void On씬이동() // view 찾아서 연결.
    {
        views = FindObjectsOfType<InventoryView>().ToList();
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
        return model.ID2stack;
    }

    public void 특정아이템정보변경(int id)
    {
        foreach(var view in views)
        {
            view.특정아이템정보갱신(id);
        }
    }
}
