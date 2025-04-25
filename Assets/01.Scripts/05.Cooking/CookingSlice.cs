using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class CookingSlice : MonoBehaviour
{
    [SerializeField] private CookingKnife knife;
    [SerializeField] private GameObject potato; // 임시
    [SerializeField] private Material cutMaterial; // 잘린 단면의 메테리얼

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knife.SliceObject(potato, cutMaterial);
        }
    }
}

