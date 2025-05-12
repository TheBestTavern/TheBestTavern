using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoSingleton<FishingManager>
{
    public TensionGauge tensionGauge;
    public FishingLineController fishingLineController;
    [SerializeField] private GameObject fishingRod;
    public bool fishingStart = false;

    void Start()
    {

    }

    public void BeginFishing()
    {
        fishingStart = true;
    }

    public void EndFishing() 
    { 

    }

    async public void UnLoadMiniGame()
    {
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
    }

}
