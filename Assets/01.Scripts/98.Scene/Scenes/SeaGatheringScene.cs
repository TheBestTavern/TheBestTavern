using UnityEngine;

public class SeaGatheringScene : BaseScene
{
    public async override void OnEixtScene()
    {
        base.OnEixtScene();

        for (int i = 0; i < 10; i++)
        {
            await CommandManager.Instance.ExecuteCommands();
        }
    }

    public override void OnEnterScene()
    {
        base.OnEnterScene();
    }

    public override void OnLoadingScene()
    {
        base.OnLoadingScene();
    }
}