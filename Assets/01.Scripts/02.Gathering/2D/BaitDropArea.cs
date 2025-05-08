using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaitDropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage;
    [SerializeField] private BaitThrowController throwController;
    private InventorySlot previousSlot; 
    public ItemStack currentBait;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (draggedSlot != null && draggedSlot.HasItem)
        {
            if (currentBait != null && previousSlot != null)
            {
                previousSlot.GetSlotItem()?.Add(1, 10);
                previousSlot.슬롯갱신(); 
            }

            currentBait = draggedSlot.GetSlotItem();
            currentBait?.Subtract(1);
            draggedSlot.슬롯갱신();
            previewImage.sprite = Resources.Load<Sprite>($"Item/{currentBait.Origin.englishName}");
            previewImage.color = Color.white;
            previousSlot = draggedSlot;
            throwController.SetBaitIndex(currentBait);
        }
    }

    public ItemStack GetCurrentBait() => currentBait;

    

    public void ClearBait()
    {
        currentBait = null;
        previewImage.sprite = null;
        previewImage.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dropAreaImage.color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropAreaImage.color = Color.white;
    }
}
