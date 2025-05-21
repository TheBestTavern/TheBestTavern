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
        blockUI = GameObject.Instantiate(go, uiManager.transform).GetComponent<BlockUI>();
        //blockUI = await AddressablesLoader.Instance.AddressablesLoadAsync<BlockUI>("BlockUI.prefab");
    }

    public void Dispose()
    {
        EventBus.UnSubscribe<EnterNightUIBlockEvent>(ShowBlock);
        EventBus.UnSubscribe<EndNightUIBlockEvent>(HideBlock);
    }

    public void ShowBlock(EnterNightUIBlockEvent evt)
    {
        if (blockUI != null)
            blockUI.gameObject.SetActive(true);
    }

    public void HideBlock(EndNightUIBlockEvent evt)
    {
        if (blockUI != null)
            blockUI.gameObject.SetActive(false);
    }
}