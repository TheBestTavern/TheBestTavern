using Cysharp.Threading.Tasks;
using UnityEngine;

public class ForestGatheringScene : BaseScene
{
    public async override UniTask OnExitScene()
    {
        await base.OnExitScene();

        for (int i = 0; i < 10; i++)
        {
            await CommandManager.Instance.ExecuteCommands();
        }
    }
}
