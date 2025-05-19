using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerContainer", menuName ="SOcontainer/new ManagerContainer")]
public class ManagerContainer : ScriptableObject
{
    public AnimationCurve saturationCurve;
    public AnimationCurve lightnessCurve;
    public Material nightMaterial;
}
