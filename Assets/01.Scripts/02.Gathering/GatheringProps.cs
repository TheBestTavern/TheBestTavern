using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GatheringProps : MonoBehaviour
{
    protected bool isClicked = false;

    private void OnMouseEnter()
    {
        if (!isClicked)
        {
            OnMouseFunc();
        }
    }

    private void OnMouseExit()
    {
        if (!isClicked)
        {
            OffMouseFunc(); 
        }
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            OnClickedFunc();
            isClicked = true;
        }
    }

    protected virtual void OnMouseFunc()
    {

    }

    protected virtual void OffMouseFunc()
    {

    }

    protected virtual void OnClickedFunc()
    {
        Debug.Log($"{gameObject.name} 클릭");
    }

}
