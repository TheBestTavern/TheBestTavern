using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public CookingSceneUI cookingSceneUI;
    public GatheringSceneUI gatheringSceneUI;
    public MainSceneUI mainSceneUI;
    public MiniGameUI miniGameUI;
    public StartSceneUI startSceneUI;

    public BlockUIManager blockUIManager;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);

        blockUIManager = new BlockUIManager();
        blockUIManager.Init(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        blockUIManager.Dispose();
    }
}
