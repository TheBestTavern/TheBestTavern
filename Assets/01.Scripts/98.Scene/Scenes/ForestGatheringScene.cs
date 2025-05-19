using UnityEngine;

public class ForestGatheringScene : BaseScene
{
    public async override void OnEixtScene()
    {
        base.OnEixtScene();

        for (int i = 0; i < 10; i++)
        {
            await CommandManager.Instance.ExecuteCommands();
        }
    }

    public async override void OnEnterScene()
    {
        base.OnEnterScene();
    }

    public async override void OnLoadingScene()
    {
        base.OnLoadingScene();
    }
}
