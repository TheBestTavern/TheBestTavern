using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoSingleton<FishingManager>
{
    public TensionGauge tensionGauge;
    public FishingLineController fishingLineController;
    [SerializeField] private GameObject fishingRod;

    void Start()
    {

    }

    public void StartFishing()
    {

    }

    public void EndFishing() 
    { 

    }

}
