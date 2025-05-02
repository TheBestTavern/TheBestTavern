using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaHitImage : Image
{
    [Range(0f, 1f)]
    public float alphaThreshold = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
