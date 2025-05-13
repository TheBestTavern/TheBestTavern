using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishingBaitDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("아이템 Drop 설정")]
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage;

    [Header("컨트롤러 설정")]
    [SerializeField] private FishingController fishingController;

    private InventorySlot previousSlot;
    private ItemStack currentBait;

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
            fishingController.SetBait(currentBait);
        }
    }

    public ItemStack GetCurrentBait() => currentBait;



    public void ClearBait()
    {
        currentBait = null;
        previewImage.sprite = null;
        previewImage.color = new Color(111, 111, 111, 0);
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
