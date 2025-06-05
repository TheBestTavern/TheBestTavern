using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoSingleton<FishingManager>
{
    public TensionGaugeController tensionGauge;
    public FishingLineController fishingLineController;
    [SerializeField] private GameObject fishingRod;
    public bool fishingStart = false;
    public bool success;
    public int gatheringKey;
    [SerializeField] private Button infoButton;

    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();
        infoButton.onClick.AddListener(OnClickInfoButton);

        exitButton.onClick.AddListener(UnLoadMiniGame);
    }


    private void Start()
    {
        success = false;
    }
 
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PopUpManager.Instance.PopUps.Remove(PopUpType.GatheringResult);
    }

    public void BeginFishing()
    {
        fishingStart = true;
    }

    public void FinishFishing()
    {
        fishingStart = false;
    }

    async public void ShowResult()
    {
        await PopUpManager.Instance.ShowPopUp(PopUpType.GatheringResult);
    }

    public async void UnLoadMiniGame()
    {
        UIManager.Instance.gatheringSceneUI.SetMiniGameBackGround(false);
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
    }
    private async void OnClickInfoButton()
    {
        await PopUpManager.Instance.ShowPopUp(PopUpType.FishingInfo);
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
