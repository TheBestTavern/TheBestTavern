using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTrashCan : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    Color defaultColor;

    private void Start()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    public async void OnDrop(PointerEventData eventData)
    {
        InventorySlot fromSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
        Debug.Log($"{fromSlot.GetSlotItem().Origin.name} 버리기");
        var popup = (ConfirmPopUp)await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
        popup.SetConfirm<int>("버릴 갯수를 입력해주세요.", (number) =>
        {
            bool success = InventoryManager.Instance.Invens[InvenType.Player].아이템잃음(fromSlot.GetSlotItem().Origin, number);
            return success;
        });

        OnPointerExit(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColor;
    }
}
