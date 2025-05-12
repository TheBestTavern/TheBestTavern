using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public async virtual void OnEixtScene()
    {
        foreach (var controller in InventoryManager.Instance.Invens)
        {
            controller.Value.On씬이동Before();
        }
    }

    public async virtual void OnEnterScene()
    {
        foreach(var controller in InventoryManager.Instance.Invens)
        {
            controller.Value.On씬이동After();
        }

        TimerManager.Instance.OnSceneMove();
        UIManager.Instance.OnSceneMove();
    }

    public async virtual void OnLoadingScene()
    {

    }
}