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

    protected override void Awake()
    {
        base.Awake();
        infoButton.onClick.AddListener(OnClickInfoButton);
    }

    private void Start()
    {
        success = false;
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
        PopUpManager.Instance.OnSceneMove();
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
