using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public CookingSceneUI cookingSceneUI;
    public GatheringSceneUI gatheringSceneUI;
    public MainSceneUI mainSceneUI;
    public MiniGameUI miniGameUI;
    public StartSceneUI startSceneUI;
}
