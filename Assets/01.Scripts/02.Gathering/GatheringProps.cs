using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GatheringProps : MonoBehaviour
{
    Animator animator;

    private void Update()
    {
        
    }

    public void OnMouseAnim()
    {
        gameObject.transform.DOScale(1.1f, 0.5f);
    }

    public void OnClickAnim()
    {
        gameObject.transform.DOShakeScale(1f, 1f);
    }

    public void ExitMouseAnim()
    {
        gameObject.transform.DOScale(1, 0.5f);
    }
}
