using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBookSlot<TData> : MonoBehaviour
{
    protected int foodCatergoryID;
    BookUI bookUI;

    [SerializeField] protected Button detailBtn;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI foodName;
    [SerializeField] protected TextMeshProUGUI desc;

    public virtual void SetSlot(TData thing)
    {
    }

    public virtual void Init(BookUI bookUI)
    {
        this.bookUI = bookUI;
        detailBtn.onClick.AddListener(() =>
        {
            bookUI.TriggerClickSlotEvent(foodCatergoryID);
        });
    }
}