using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectMapButtonHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    TextMeshProUGUI regionText;

    private void Awake()
    {
        image = GetComponent<Image>();
        regionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f,1.2f,1.2f),0.5f);        
        image.DOColor(new Color(0, 0, 0), 0.5f);
        regionText.DOFade(1,0.5f);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        image.DOColor(new Color(1, 1, 1), 0.5f);
        regionText.DOFade(0, 0.5f);
    }

}
