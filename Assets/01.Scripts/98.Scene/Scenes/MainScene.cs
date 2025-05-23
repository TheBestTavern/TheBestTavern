using UnityEngine;

public class MainScene : BaseScene
{
    public async override void OnEixtScene()
    {
        base.OnEixtScene();
    }

    public async override void OnEnterScene()
    {
        base.OnEnterScene();
        SoundManager.Instance.PlayBGM("MainBGM1");
        SoundManager.Instance.StopLoop();
    }

    public async override void OnLoadingScene()
    {
        base.OnLoadingScene();
    }
}
