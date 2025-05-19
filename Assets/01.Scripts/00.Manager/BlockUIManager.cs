using static Unity.Collections.AllocatorManager;
using UnityEngine;

public class BlockUIManager
{
    BlockUI blockUI;
    UIManager uiManager;

    public async void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
        EventBus.Subscribe<EnterNightUIBlockEvent>(ShowBlock);
        EventBus.Subscribe<EndNightUIBlockEvent>(HideBlock);
        GameObject go = await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>("BlockUI.prefab");
        blockUI = GameObject.Instantiate(go, uiManager.transform).GetComponentInChildren<BlockUI>();
    }

    public void Dispose()
    {
        EventBus.UnSubscribe<EnterNightUIBlockEvent>(ShowBlock);
        EventBus.UnSubscribe<EndNightUIBlockEvent>(HideBlock);
    }

    public void ShowBlock(EnterNightUIBlockEvent evt)
    {
        blockUI.gameObject.SetActive(true);
    }

    public void HideBlock(EndNightUIBlockEvent evt)
    {
        blockUI.gameObject.SetActive(false);
    }
}