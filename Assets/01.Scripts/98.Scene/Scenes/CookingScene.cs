using Cysharp.Threading.Tasks;
using UnityEngine;

public class CookingScene : BaseScene
{

    public async override UniTask OnEnterScene()
    {
        await base.OnEnterScene();
        SoundManager.Instance.StopLoop();
    }
}
