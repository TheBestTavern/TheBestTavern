using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : Image, IPoolable
{
    public string ID => gameObject.name; 
    
    public bool CanDec => true;

    public float DecPeriod => 10;

    public event Action<IPoolable> OnReturn;

    public RectTransform rect;

    public void Initialize(Action<IPoolable> a)
    {
        OnReturn = a;
        rect = GetComponent<RectTransform>();
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void OnSpawn(Vector3 pos)
    {
        Debug.Log("출격");
        rect.position = pos;
    }

    public void TriggerReturn()
    {
        OnReturn?.Invoke(this);
    }
}
