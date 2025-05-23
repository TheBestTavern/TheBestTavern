using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class IntroScene : BaseScene
{
    public async override UniTask OnExitScene()
    {
        await base.OnExitScene();
        try
        {
            await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("AlreadyLoad");
        }
        catch (Exception e)
        {
            Debug.LogError($"Addressables Load Failed: {e.Message}");
        }
    }
}
