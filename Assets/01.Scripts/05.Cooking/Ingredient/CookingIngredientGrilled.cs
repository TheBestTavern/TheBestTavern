using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CookingIngredientGrilled : CookingIngrediantBase, IGrillable
{
    public event Action<IGrillable> OnGrilled;

    public void GrilledEffect()
    {
        OnGrilled?.Invoke(this);
    }
    public void ApplyForce()
    {
        float force  = 20f;
        GetComponent<Rigidbody>()?.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void ColorChange()
    {
    }

}
