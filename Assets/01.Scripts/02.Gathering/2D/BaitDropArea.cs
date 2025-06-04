using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaitDropArea : MonoBehaviour
{
    [Header("아이템 Drop 설정")]
    [SerializeField] private Image previewImage;
    [SerializeField] private Image dropAreaImage;
    public Sprite itemSprite;

    [Header("컨트롤러 설정")]
    [SerializeField] private BaitThrowController throwController;

    private InventorySlot previousSlot; 
    private ItemStack currentBait;

    private ItemStack currentItem;

    public async void SetItem(ItemStack item)
    {
        currentBait = item;

        if (item != null && item.OriginItemKey != null)
        {
            Debug.Log("BaitDropArea에 아이템 설정됨: " + item.OriginItemKey);

            Data_Foods raw = Data.GetRawItem(item.OriginItemKey);

            Sprite loadedSprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", raw.englishName, true);

            if (loadedSprite != null)
            {
                previewImage.sprite = loadedSprite;
                previewImage.color = Color.white;
                itemSprite = loadedSprite;
            }
            else
            {
                Debug.LogWarning($"이미지를 로드하지 못했습니다: {raw.englishName}");
                previewImage.sprite = null;
                previewImage.color = new Color(1f, 1f, 1f, 0f);
            }

            throwController.SetBaitIndex(item);
        }
        else
        {
            Debug.LogWarning("SetItem에 전달된 아이템이 null입니다.");
        }
    }

    public void ClearBait()
    {
        currentBait = null;
        previewImage.sprite = null;
        previewImage.color = new Color(111, 111, 111, 0);
    }

    
}
