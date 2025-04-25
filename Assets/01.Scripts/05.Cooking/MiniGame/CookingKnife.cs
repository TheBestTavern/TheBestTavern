using System.Collections;
using System.Collections.Generic;
using EzySlice;
using Unity.VisualScripting;
using UnityEngine;

public class CookingKnife : MonoBehaviour
{

    public SlicedHull SliceObject(GameObject obj, Material material)
    {
        var slicedObj = obj.Slice(transform.position, transform.up, material);

        if (slicedObj != null)
        {
            GameObject upper = slicedObj.CreateUpperHull(obj, material);
            GameObject lower = slicedObj.CreateLowerHull(obj, material);

            upper.transform.position = obj.transform.position;
            lower.transform.position = obj.transform.position;

            obj.SetActive(false);
        }

        return obj.Slice(transform.position, transform.up, material);
    }
}
