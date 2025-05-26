using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

public class IntroScene : BaseScene
{
    public async override UniTask OnExitScene()
    {
        await base.OnExitScene();
        try
        {
            //await AddressablesLoader.Instance.AddressablesLoadAsync<SpriteAtlas>("AlreadyLoad");
            await AddressablesLoader.Instance.PreloadAllFromLavelAsync("AlreadyLoad");
        }
        catch (Exception e)
        {
            Debug.LogError($"Addressables Load Failed: {e.Message}");
        }
    }
}
