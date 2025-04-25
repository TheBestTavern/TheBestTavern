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
        model.Init(slotMaxCount, perStackMaxCount);
        DontDestroyOnLoad(gameObject);
        On씬이동();
    }

    public virtual void On씬이동() // view 찾아서 연결.
    {
        views = FindObjectsOfType<InventoryView>().ToList();
    }

    public virtual void 아이템획득(Data_Foods data_Foods, int amount)
    {
        model.아이템검사후추가(data_Foods, amount);
    }

    public virtual void 아이템잃음(Data_Foods data_Foods, int amount)
    {
        model.아이템감소(data_Foods, amount);
    }

    public void 아이템정렬_합치기()
    {
        model.아이템정렬_합치기();
    }

    public virtual void 아이템띄우기()
    {
        model.
    }
}
