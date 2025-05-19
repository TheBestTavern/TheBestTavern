using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class BaseBookSlot<TData> : MonoBehaviour
{
    protected int foodCatergoryID;

    [SerializeField] protected Button detailBtn;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI foodName;
    [SerializeField] protected TextMeshProUGUI desc;
    [SerializeField] protected TextMeshProUGUI title;

    public virtual void SetSlot(TData thing)
    {
    }

    public virtual void Init(BookUI bookUI)
    {
        detailBtn.onClick.AddListener(() =>
        {
            bookUI.TriggerClickSlotEvent(foodCatergoryID);
        });
    }
    protected async virtual void SetUndiscoveredItem()
    {
        foodCatergoryID = 0;
        detailBtn.interactable = false;
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Undiscovered2.Sprite");
        foodName.text = "미발견";
        desc.text = "";
    }
}