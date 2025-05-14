using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoSingleton<FishingManager>
{
    public TensionGaugeController tensionGauge;
    public FishingLineController fishingLineController;
    [SerializeField] private GameObject fishingRod;
    public bool fishingStart = false;
    public bool success;
    public int gatheringKey;

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

    async public void ShowResult()
    {
        await PopUpManager.Instance.ShowPopUp(PopUpType.GatheringResult);
    }

    async public void UnLoadMiniGame()
    {
        UIManager.Instance.gatheringSceneUI.SetMiniGameBackGround(false);
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
    }

    public bool GetResult()
    {
        return success;
    }

    public int GetGatheringKey()
    {
        return gatheringKey;
    }

}
