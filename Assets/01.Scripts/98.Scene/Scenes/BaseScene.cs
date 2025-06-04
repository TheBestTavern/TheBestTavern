using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseScene
{
    public virtual UniTask OnExitScene()
    {
        foreach (var controller in InventoryManager.Instance.Invens)
        {
            controller.Value.OnBeforeSceneMove();
        }
        PopUpManager.Instance.OnSceneMove();
        return UniTask.CompletedTask;
    }

    public virtual UniTask OnEnterScene()
    {
        //foreach (var controller in InventoryManager.Instance.Invens)
        //{
        //    controller.Value.OnAfterSceneMove();
        //}

        TimerManager.Instance.OnSceneMove();
        return UniTask.CompletedTask;

    }

    public virtual UniTask OnLoadingScene()
    {
        return UniTask.CompletedTask;
    }
}