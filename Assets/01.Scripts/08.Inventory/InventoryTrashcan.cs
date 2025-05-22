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

        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent<InventorySlot>(out InventorySlot fromSlot))
        {
            Debug.Log($"{fromSlot.GetSlotItem().Origin.name} 버리기");
            var popup = (ConfirmPopUp)await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
            popup.SetConfirm<int>("버릴 갯수를 입력해주세요.", (number) =>
            {
                bool success = InventoryManager.Instance.Invens[InvenType.Player].ThrowInTrash(fromSlot.GetSlotItem().Origin, number);
                return success;
            });

            OnPointerExit(eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            image.color = new Color(1, 1, 1, 1);
            gameObject.transform.localScale = new Vector2(1.3f, 1.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColor;
        gameObject.transform.localScale = Vector3.one;
    }
}
