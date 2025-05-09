using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableMonoBehaviour : MonoBehaviour, IDragHandler
{
    [Header("드래그")]
    [SerializeField] RectTransform draggableObjectTrs;
    [SerializeField] bool draggable = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable && IsIn(eventData.position))
            draggableObjectTrs.anchoredPosition += eventData.delta;
        //rectTransform.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    private bool IsIn(Vector3 cursorPos)
    {
        return (cursorPos.x < Screen.width && cursorPos.x > 0 && cursorPos.y < Screen.height && cursorPos.y > 0);
    }
}
