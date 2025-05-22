using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public virtual void OnEixtScene()
    {
        foreach (var controller in InventoryManager.Instance.Invens)
        {
            controller.Value.OnBeforeSceneMove();
        }
        PopUpManager.Instance.OnSceneMove();
    }

    public virtual void OnEnterScene()
    {
        foreach(var controller in InventoryManager.Instance.Invens)
        {
            controller.Value.OnAfterSceneMove();
        }

        TimerManager.Instance.OnSceneMove();       
    }

    public virtual void OnLoadingScene()
    {

    }
}