using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    Data_Foods raw;

    private void Awake()
    {
        Clear();
    }

    public void SetSlot(Data_Foods item)
    {
        this.raw = item;
        image.sprite = Resources.Load<Sprite>($"Item/{raw.englishName}");
        image.color = new Color(1.2f, 1.2f, 1.2f, 1);
    }

    public Data_Foods GetSlotItem()
    {
        return raw;
    }

    public void Clear()
    {
        raw = null;
        image.color = new Color(1, 1, 1, 0);
    }
}
