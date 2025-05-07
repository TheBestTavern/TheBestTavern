using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaitDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage; 
    [SerializeField] private BaitThrowController throwController;

    private ItemStack currentBait;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (draggedSlot != null && draggedSlot.HasItem)
        {
            currentBait = draggedSlot.GetSlotItem();

            previewImage.sprite = Resources.Load<Sprite>($"Item/{currentBait.Origin.englishName}");

            if (previewImage != null)
            {
                previewImage.color = Color.white;  
                throwController.SetBaitIndex(currentBait);
            }
            else
            {
                Debug.LogError("실패: " + currentBait.Origin.englishName);
            }
        }
    }

    public ItemStack GetCurrentBait() => currentBait;

    public void UseOneBait()
    {
        if (currentBait == null) return;

        currentBait.Subtract(1);
        if (currentBait.Count <= 0)
        {
            ClearBait();
        }
    }

    public void ClearBait()
    {
        currentBait = null;
        previewImage.sprite = null;
        previewImage.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropAreaImage.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropAreaImage.color = Color.white;
    }
}
