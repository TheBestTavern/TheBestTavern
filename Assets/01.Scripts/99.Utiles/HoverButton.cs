using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button mapBtn;
    [SerializeField] Button MainSceneBtn;

    RectTransform rect;
    RectTransform mapRect;
    RectTransform MainSceneRect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        mapRect = mapBtn.GetComponent<RectTransform>();
        MainSceneRect = MainSceneBtn.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOSizeDelta(new Vector2(550, rect.sizeDelta.y), 0.5f);
        mapRect.DOSizeDelta(new Vector2(100, mapRect.sizeDelta.y), 0.5f);
        MainSceneRect.DOSizeDelta(new Vector2(100, MainSceneRect.sizeDelta.y), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOSizeDelta(new Vector2(0, rect.sizeDelta.y), 0.5f);
        mapRect.DOSizeDelta(new Vector2(0, mapRect.sizeDelta.y), 0.5f);
        MainSceneRect.DOSizeDelta(new Vector2(0, MainSceneRect.sizeDelta.y), 0.5f);
    }
}
