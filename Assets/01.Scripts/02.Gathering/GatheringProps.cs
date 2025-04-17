using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GatheringProps : MonoBehaviour
{
    private void OnMouseEnter()
    {
        gameObject.transform.DOScale(1.1f, 0.5f);
    }

    private void OnMouseExit()
    {
        gameObject.transform.DOScale(1, 0.5f);
    }

    private void OnMouseDown()
    {
        gameObject.transform.DOShakeScale(1f, 1f);
    }
}
