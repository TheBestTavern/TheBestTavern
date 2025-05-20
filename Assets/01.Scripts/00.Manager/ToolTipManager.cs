using UnityEngine;

public class ToolTipManager
{
    ToolTip tooltip;

    public async void Initialize()
    {
        GameObject pref = await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>("ToolTip.prefab");
        tooltip = GameObject.Instantiate(pref, PopUpManager.Instance.gameObject.transform).GetComponent<ToolTip>();

        EventBus.Subscribe<SlotHoverEnterEvent>((evt) => tooltip.ShowToolTip(evt));
        EventBus.Subscribe<SlotHoverEndEvent>((evt) => tooltip.HideToolTip(evt));
    }

    public void OnDispose()
    {
        EventBus.UnSubscribe<SlotHoverEnterEvent>((evt) => tooltip.ShowToolTip(evt));
        EventBus.UnSubscribe<SlotHoverEndEvent>((evt) => tooltip.HideToolTip(evt));
    }
}