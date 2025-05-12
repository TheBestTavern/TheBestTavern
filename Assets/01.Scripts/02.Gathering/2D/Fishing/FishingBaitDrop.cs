using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishingBaitDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage;
    [SerializeField] private FishingController fishingController;
    [SerializeField] private GameObject baitPrefab;
    [SerializeField] private Transform baitSpawnPoint;
    private GameObject baitObjectInstance;
    private InventorySlot previousSlot;
    public ItemStack currentBait;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (draggedSlot != null && draggedSlot.HasItem)
        {
            // 기존 미끼 되돌리기
            if (currentBait != null && previousSlot != null)
            {
                previousSlot.GetSlotItem()?.Add(1, 10);
                previousSlot.슬롯갱신();
            }

            // 기존 미끼 오브젝트 제거
            if (baitObjectInstance != null)
            {
                Destroy(baitObjectInstance);
            }

            // 미끼 설정
            currentBait = draggedSlot.GetSlotItem();
            currentBait?.Subtract(1);
            draggedSlot.슬롯갱신();
            previewImage.sprite = Resources.Load<Sprite>($"Item/{currentBait.Origin.englishName}");
            previewImage.color = Color.white;
            previousSlot = draggedSlot;

            // 🧠 미끼 오브젝트 생성
            baitObjectInstance = Instantiate(baitPrefab, baitSpawnPoint.position, Quaternion.identity);

            // 🎯 FishingController로 전달 (ItemStack + GameObject)
            fishingController.SetBait(baitObjectInstance, currentBait);
        }
    }

    public ItemStack GetCurrentBait() => currentBait;



    public void ClearBait()
    {
        currentBait = null;
        previewImage.sprite = null;
        previewImage.color = new Color(1, 1, 1, 0);

        if (baitObjectInstance != null)
        {
            Destroy(baitObjectInstance);
            baitObjectInstance = null;
        }
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
