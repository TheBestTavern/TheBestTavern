using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaitDropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("아이템 Drop 설정")]
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage;

    [Header("컨트롤러 설정")]
    [SerializeField] private BaitThrowController throwController;

    private InventorySlot previousSlot; 
    private ItemStack currentBait;

    public async void OnDrop(PointerEventData eventData)
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

            try
            {
                if (currentBait != null)
                {
                    Data_Foods raw = Data.GetRawItem(currentBait.Origin.key);
                    string path = $"Assets/16.Image/FoodImage/{raw.englishName}.png";
                    Sprite sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>(path, true);

                    previewImage.sprite = sprite;
                    previewImage.color = Color.white;
                }
                else
                {
                    ResetPreviewImage();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading bait image: {e}");
                ResetPreviewImage();
            }

            previousSlot = draggedSlot;

            throwController.SetBaitIndex(currentBait);
        }
    }

    private void ResetPreviewImage()
    {
        previewImage.sprite = null;
        previewImage.color = new Color(1, 1, 1, 0);
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
